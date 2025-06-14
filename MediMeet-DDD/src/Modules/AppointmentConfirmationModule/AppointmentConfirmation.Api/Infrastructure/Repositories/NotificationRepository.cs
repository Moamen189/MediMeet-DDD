using AppointmentConfirmation.Api.Domain.Models;
using AppointmentConfirmation.Api.Domain.Repositories;
using AppointmentConfirmation.Api.Infrastructure.Database;
using AppointmentConfirmation.Api.Infrastructure.Entities;
using MediatR;

namespace AppointmentConfirmation.Api.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
	private readonly IMediator _mediator;

	public NotificationRepository(IMediator mediator)
	{
		_mediator = mediator;
	}

	public Task<NotificationEntity> GetByIdAsync(NotificationId id)
	{
		var entity = InMemoryDb.Notifications.FirstOrDefault(n => n.Id == id.Value);
		if (entity == null)
		{
			throw new CannotUnloadAppDomainException($"Notification with ID {id.Value} not found");
		}

		return Task.FromResult(MapToDomain(entity));
	}

	public async Task<NotificationEntity> AddAsync(NotificationEntity notification)
	{
		var entity = MapToEntity(notification);
		InMemoryDb.Notifications.Add(entity);

		foreach (var domainEvent in notification.DomainEvents)
		{
			await _mediator.Publish(domainEvent);
		}

		return notification;
	}

	public async Task UpdateAsync(NotificationEntity notification)
	{
		var entity = InMemoryDb.Notifications.FirstOrDefault(n => n.Id == notification.Id.Value);
		if (entity == null)
		{
			throw new CannotUnloadAppDomainException($"Notification with ID {notification.Id.Value} not found");
		}

		// Update entity
		entity.Status = notification.Status.ToString();
		entity.SentAt = notification.SentAt;

		foreach (var domainEvent in notification.DomainEvents)
		{
			await _mediator.Publish(domainEvent);
		}
	}

	public Task<List<NotificationEntity>> GetPendingNotificationsAsync()
	{
		var entities = InMemoryDb.Notifications
			.Where(n => n.Status == NotificationStatus.Pending.ToString())
			.Select(MapToDomain)
			.ToList();

		return Task.FromResult(entities);
	}

	public Task<List<NotificationEntity>> GetNotificationsByRecipientAsync(string recipientEmail)
	{
		var entities = InMemoryDb.Notifications
			.Where(n => n.RecipientEmail == recipientEmail)
			.Select(MapToDomain)
			.ToList();

		return Task.FromResult(entities);
	}

	public Task<List<NotificationEntity>> GetFailedNotificationsAsync()
	{
		var entities = InMemoryDb.Notifications
			.Where(n => n.Status == NotificationStatus.Failed.ToString())
			.Select(MapToDomain)
			.ToList();

		return Task.FromResult(entities);
	}

	private static NotificationEntity MapToEntity(NotificationEntity notification)
	{
		return new NotificationEntity
		{
			Id = notification.Id,
			Type = notification.Type.ToString(),
			Subject = notification.Subject,
			Body = notification.Body,
			RecipientEmail = notification.RecipientEmail,
			RecipientName = notification.RecipientName,
			Status = notification.Status.ToString(),
			CreatedAt = notification.CreatedAt,
			SentAt = notification.SentAt
		};
	}

	private static NotificationEntity MapToDomain(NotificationEntity entity)
	{
		var type = Enum.Parse<NotificationType>(entity.Type);
		var status = Enum.Parse<NotificationStatus>(entity.Status);

		var notification = type switch
		{
			NotificationType.AppointmentConfirmation => Notification.CreateAppointmentConfirmation(
				entity.RecipientEmail,
				entity.RecipientName,
				entity.CreatedAt, // Using CreatedAt as appointment time for simplicity
				"Doctor" // This should come from the actual appointment data
			),
			NotificationType.AppointmentCancellation => Notification.CreateAppointmentCancellation(
				entity.RecipientEmail,
				entity.RecipientName,
				entity.CreatedAt,
				"Doctor",
				"Cancelled by doctor" // This should come from the actual cancellation reason
			),
			NotificationType.AppointmentReminder => Notification.CreateAppointmentReminder(
				entity.RecipientEmail,
				entity.RecipientName,
				entity.CreatedAt,
				"Doctor"
			),
			_ => throw new CannotUnloadAppDomainException($"Unknown notification type: {entity.Type}")
		};

		// Handle status
		if (status == NotificationStatus.Sent)
		{
			notification.MarkAsSent();
		}
		else if (status == NotificationStatus.Failed)
		{
			notification.MarkAsFailed("Unknown error"); // This should come from actual failure reason
		}

		return notification;
	}
}