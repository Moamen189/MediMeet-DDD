using AppointmentConfirmation.Api.Domain.Models;
using AppointmentConfirmation.Api.Domain.Repositories;
using AppointmentConfirmation.Api.Infrastructure.Database;
using AppointmentConfirmation.Api.Infrastructure.Entities;
using MediatR;
using Shared.RootEntity;

namespace AppointmentConfirmation.Api.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
	private readonly IMediator _mediator;

	public NotificationRepository(IMediator mediator)
	{
		_mediator = mediator;
	}

	public Task<Notification> GetByIdAsync(NotificationId id)
	{
		var entity = InMemoryDb.Notifications.FirstOrDefault(n => n.Id == id.Value);
		if (entity == null)
		{
			throw new DomainException($"Notification with ID {id.Value} not found");
		}

		return Task.FromResult(MapToDomain(entity));
	}

	public async Task<Notification> AddAsync(Notification notification)
	{
		var entity = MapToEntity(notification);
		InMemoryDb.Notifications.Add(entity);

		foreach (var domainEvent in notification.DomainEvents)
		{
			await _mediator.Publish(domainEvent);
		}

		return notification;
	}

	public async Task UpdateAsync(Notification notification)
	{
		var entity = InMemoryDb.Notifications.FirstOrDefault(n => n.Id == notification.Id.Value);
		if (entity == null)
		{
			throw new DomainException($"Notification with ID {notification.Id.Value} not found");
		}

		// Update entity
		entity.Status = notification.Status.ToString();
		entity.SentAt = notification.SentAt;

		foreach (var domainEvent in notification.DomainEvents)
		{
			await _mediator.Publish(domainEvent);
		}
	}

	public Task<List<Notification>> GetPendingNotificationsAsync()
	{
		var entities = InMemoryDb.Notifications
			.Where(n => n.Status == NotificationStatus.Pending.ToString())
			.Select(MapToDomain)
			.ToList();

		return Task.FromResult(entities);
	}

	public Task<List<Notification>> GetNotificationsByRecipientAsync(string recipientEmail)
	{
		var entities = InMemoryDb.Notifications
			.Where(n => n.RecipientEmail == recipientEmail)
			.Select(MapToDomain)
			.ToList();

		return Task.FromResult(entities);
	}

	public Task<List<Notification>> GetFailedNotificationsAsync()
	{
		var entities = InMemoryDb.Notifications
			.Where(n => n.Status == NotificationStatus.Failed.ToString())
			.Select(MapToDomain)
			.ToList();

		return Task.FromResult(entities);
	}

	private static NotificationEntity MapToEntity(Notification notification)
	{
		return new NotificationEntity
		{
			Id = notification.Id.Value,
			Type = notification.Type.ToString(),
			Subject = notification.Content.Subject,
			Body = notification.Content.Body,
			RecipientEmail = notification.Recipient.Email,
			RecipientName = notification.Recipient.Name,
			Status = notification.Status.ToString(),
			CreatedAt = notification.CreatedAt,
			SentAt = notification.SentAt
		};
	}

	private static Notification MapToDomain(NotificationEntity entity)
	{
		var type = Enum.Parse<NotificationMessageType>(entity.Type);
		var status = Enum.Parse<NotificationStatus>(entity.Status);

		var recipient = NotificationRecipient.Create(entity.RecipientEmail, entity.RecipientName);
		var content = NotificationContent.Create(entity.Subject, entity.Body);

		var notification = Notification.Create(content, recipient, type);

		// Handle status
		if (status == NotificationStatus.Sent)
		{
			notification.MarkAsSent();
		}
		else if (status == NotificationStatus.Failed)
		{
			notification.MarkAsFailed();
		}

		return notification;
	}
}