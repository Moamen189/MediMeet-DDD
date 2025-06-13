using AppointmentConfirmation.Api.Domain.Models;

namespace AppointmentConfirmation.Api.Domain.Repositories;

public interface INotificationRepository
{
    Task<Notification> GetByIdAsync(NotificationId id);
    Task<Notification> AddAsync(Notification notification);
    Task UpdateAsync(Notification notification);
    Task<List<Notification>> GetPendingNotificationsAsync();
    Task<List<Notification>> GetNotificationsByRecipientAsync(string recipientEmail);
    Task<List<Notification>> GetFailedNotificationsAsync();
} 