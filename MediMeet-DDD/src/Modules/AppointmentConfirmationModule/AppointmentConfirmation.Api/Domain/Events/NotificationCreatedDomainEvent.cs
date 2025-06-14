using AppointmentConfirmation.Api.Domain.Models;

namespace AppointmentConfirmation.Api.Domain.Events;

public record NotificationCreatedDomainEvent
{
    public NotificationId NotificationId { get; }
    public NotificationContent Content { get; }
    public NotificationRecipient Recipient { get; }
    public NotificationMessageType Type { get; }
    public DateTime CreatedAt { get; }

    public NotificationCreatedDomainEvent(Notification notification)
    {
        NotificationId = notification.Id;
        Content = notification.Content;
        Recipient = notification.Recipient;
        Type = notification.Type;
        CreatedAt = notification.CreatedAt;
    }
} 