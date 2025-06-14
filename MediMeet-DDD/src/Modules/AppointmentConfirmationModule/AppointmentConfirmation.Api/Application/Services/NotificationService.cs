using AppointmentConfirmation.Api.Domain.Models;
using AppointmentConfirmation.Api.Domain.Repositories;
using Shared.RootEntity;

namespace AppointmentConfirmation.Api.Application.Services;

public class NotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task SendAppointmentConfirmationAsync(
        string recipientEmail,
        string recipientName,
        DateTime appointmentDateTime,
        string doctorName)
    {
        var recipient = NotificationRecipient.Create(recipientEmail, recipientName);
        var content = NotificationContent.CreateAppointmentConfirmation(
            recipientName,
            appointmentDateTime,
            doctorName);

        var notification = Notification.Create(
            content,
            recipient,
            NotificationMessageType.AppointmentConfirmation);

        await _notificationRepository.AddAsync(notification);
    }

    public async Task SendAppointmentCancellationAsync(
        string recipientEmail,
        string recipientName,
        DateTime appointmentDateTime,
        string doctorName,
        string cancellationReason)
    {
        var recipient = NotificationRecipient.Create(recipientEmail, recipientName);
        var content = NotificationContent.CreateAppointmentCancellation(
            recipientName,
            appointmentDateTime,
            doctorName,
            cancellationReason);

        var notification = Notification.Create(
            content,
            recipient,
            NotificationMessageType.AppointmentCancellation);

        await _notificationRepository.AddAsync(notification);
    }

    public async Task SendAppointmentReminderAsync(
        string recipientEmail,
        string recipientName,
        DateTime appointmentDateTime,
        string doctorName)
    {
        var recipient = NotificationRecipient.Create(recipientEmail, recipientName);
        var content = NotificationContent.CreateAppointmentReminder(
            recipientName,
            appointmentDateTime,
            doctorName);

        var notification = Notification.Create(
            content,
            recipient,
            NotificationMessageType.AppointmentReminder);

        await _notificationRepository.AddAsync(notification);
    }

    public async Task<List<Notification>> GetPendingNotificationsAsync()
    {
        return await _notificationRepository.GetPendingNotificationsAsync();
    }

    public async Task<List<Notification>> GetFailedNotificationsAsync()
    {
        return await _notificationRepository.GetFailedNotificationsAsync();
    }

    public async Task MarkAsSentAsync(NotificationId id)
    {
        var notification = await _notificationRepository.GetByIdAsync(id);
        notification.MarkAsSent();
        await _notificationRepository.UpdateAsync(notification);
    }

    public async Task MarkAsFailedAsync(NotificationId id)
    {
        var notification = await _notificationRepository.GetByIdAsync(id);
        notification.MarkAsFailed();
        await _notificationRepository.UpdateAsync(notification);
    }
} 