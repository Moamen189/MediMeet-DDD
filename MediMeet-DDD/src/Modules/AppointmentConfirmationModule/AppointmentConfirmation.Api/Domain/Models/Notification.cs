using Shared.RootEntity;

namespace AppointmentConfirmation.Api.Domain.Models
{
    public class Notification : Entity
    {
        private Notification() { }

        private Notification(
            NotificationId id,
            NotificationContent content,
            NotificationRecipient recipient,
            NotificationMessageType type)
        {
            Id = id;
            Content = content;
            Recipient = recipient;
            Type = type;
            Status = NotificationStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public NotificationId Id { get; private set; }
        public NotificationContent Content { get; private set; }
        public NotificationRecipient Recipient { get; private set; }
        public NotificationMessageType Type { get; private set; }
        public NotificationStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? SentAt { get; private set; }

        public static Notification Create(
            NotificationContent content,
            NotificationRecipient recipient,
            NotificationMessageType type)
        {
            return new Notification(
                NotificationId.Create(),
                content,
                recipient,
                type
            );
        }

        public void MarkAsSent()
        {
            if (Status != NotificationStatus.Pending)
            {
                throw new DomainException("Can only mark pending notifications as sent");
            }

            Status = NotificationStatus.Sent;
            SentAt = DateTime.UtcNow;
        }

        public void MarkAsFailed()
        {
            if (Status != NotificationStatus.Pending)
            {
                throw new DomainException("Can only mark pending notifications as failed");
            }

            Status = NotificationStatus.Failed;
        }
    }

    public enum NotificationType
    {
        AppointmentConfirmation,
        AppointmentReminder,
        AppointmentCancellation
    }

    public enum NotificationStatus
    {
        Pending,
        Sent,
        Failed
    }
} 