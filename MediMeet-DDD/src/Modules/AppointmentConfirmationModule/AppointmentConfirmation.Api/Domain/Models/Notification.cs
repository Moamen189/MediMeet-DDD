using Shared.RootEntity;

namespace AppointmentConfirmation.Api.Domain.Models
{
    public class Notification : AggregateRoot
    {
        public Guid Id { get; private set; }
        public string RecipientEmail { get; private set; }
        public string RecipientName { get; private set; }
        public string Content { get; private set; }
        public NotificationType Type { get; private set; }
        public NotificationStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? SentAt { get; private set; }

        private Notification() { }

        public static Notification Create(
            string recipientEmail,
            string recipientName,
            string content,
            NotificationType type)
        {
            return new Notification
            {
                Id = Guid.NewGuid(),
                RecipientEmail = recipientEmail,
                RecipientName = recipientName,
                Content = content,
                Type = type,
                Status = NotificationStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void MarkAsSent()
        {
            Status = NotificationStatus.Sent;
            SentAt = DateTime.UtcNow;
        }

        public void MarkAsFailed()
        {
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