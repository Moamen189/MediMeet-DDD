using AppointmentConfirmation.Api.Domain.Models;
using AppointmentConfirmation.Api.Infrastructure.Entities;

namespace AppointmentConfirmation.Api.Domain.Repositories;

public interface INotificationRepository
{
	Task<NotificationEntity> GetByIdAsync(NotificationId id);
	Task<NotificationEntity> AddAsync(NotificationEntity notification);
	Task UpdateAsync(NotificationEntity notification);
	Task<List<NotificationEntity>> GetPendingNotificationsAsync();
	Task<List<NotificationEntity>> GetNotificationsByRecipientAsync(string recipientEmail);
	Task<List<NotificationEntity>> GetFailedNotificationsAsync();
}