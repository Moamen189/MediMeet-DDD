using AppointmentConfirmation.Api.Domain.Models;
using Shared.EventBus;

namespace AppointmentConfirmation.Api.Domain.Events;

public record NotificationCreatedDomainEvent : IDomainEvent
{
    public NotificationCreatedDomainEvent(Notification notification)
    {
        NotificationId = notification.Id;
        Type = notification.Type;
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
    public NotificationSentDomainEvent(Notification notification)
    {
        NotificationId = notification.Id;
        Type = notification.Type;
        RecipientEmail = notification.Recipient.Email;
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
        NotificationId = notification.Id;
        Type = notification.Type;
        RecipientEmail = notification.Recipient.Email;
        FailureReason = reason;
        FailedAt = DateTime.UtcNow;
    }

    public NotificationId NotificationId { get; }
    public NotificationType Type { get; }
    public string RecipientEmail { get; }
    public string FailureReason { get; }
    public DateTime FailedAt { get; }
} 