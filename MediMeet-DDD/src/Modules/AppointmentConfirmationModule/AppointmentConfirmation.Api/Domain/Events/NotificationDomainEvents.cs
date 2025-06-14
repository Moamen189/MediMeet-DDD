using AppointmentConfirmation.Api.Domain.Models;
using AppointmentConfirmation.Api.Infrastructure.Entities;
using Shared.EventBus;

namespace AppointmentConfirmation.Api.Domain.Events;

public class NotificationCreatedDomainEvent : IDomainEvent
{
	public NotificationCreatedDomainEvent(Notification notification)
	{
		NotificationId = notification.Id;
		Type = (NotificationType)notification.Type;
		RecipientEmail = notification.Recipient.Email;
		RecipientName = notification.Recipient.Name;
		Subject = notification.Content.Subject;
		CreatedAt = notification.CreatedAt;
	}

	public NotificationId NotificationId { get; }
	public NotificationType Type { get; }
	public string RecipientEmail { get; }
	public string RecipientName { get; }
	public string Subject { get; }
	public DateTime CreatedAt { get; }
}

public record NotificationSentDomainEvent : IDomainEvent
{
	public NotificationSentDomainEvent(NotificationEntity notification)
	{
		NotificationId = NotificationId.From(notification.Id);
		Type = Enum.TryParse<NotificationType>(notification.Type, out var parsedType)
			? parsedType
			: throw new ArgumentException($"Invalid NotificationType: {notification.Type}", nameof(notification.Type));
		RecipientEmail = notification.RecipientEmail ?? throw new ArgumentNullException(nameof(notification.RecipientEmail));
		SentAt = notification.SentAt!.Value;
	}

	public NotificationId NotificationId { get; }
	public NotificationType Type { get; }
	public string RecipientEmail { get; }
	public DateTime SentAt { get; }
}

public record NotificationFailedDomainEvent : IDomainEvent
{
	public NotificationFailedDomainEvent(Notification notification, string reason)
	{
		NotificationId = NotificationId.From(notification.Id.Value); // Removed `.Value` as `notification.Id` is already a `Guid`.  
		Type = (NotificationType)notification.Type;
		RecipientEmail = notification.Recipient.Email ?? throw new ArgumentNullException(nameof(notification.Recipient.Email));
		FailureReason = reason;
		FailedAt = DateTime.UtcNow;
	}

	public NotificationId NotificationId { get; }
	public NotificationType Type { get; }
	public string RecipientEmail { get; }
	public string FailureReason { get; }
	public DateTime FailedAt { get; }
}
