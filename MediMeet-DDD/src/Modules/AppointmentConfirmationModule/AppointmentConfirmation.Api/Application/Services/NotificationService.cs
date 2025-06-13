using AppointmentConfirmation.Api.Domain.Models;
using AppointmentConfirmation.Api.Domain.Repositories;
using AppointmentConfirmation.Shared;
using Microsoft.Extensions.Logging;

namespace AppointmentConfirmation.Api.Application.Services;

public class NotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(
        INotificationRepository notificationRepository,
        ILogger<NotificationService> logger)
    {
        _notificationRepository = notificationRepository;
        _logger = logger;
    }

    public async Task SendAppointmentConfirmationAsync(
        string recipientEmail,
        string recipientName,
        DateTime appointmentDateTime,
        string doctorName)
    {
        var notification = Notification.CreateAppointmentConfirmation(
            recipientEmail,
            recipientName,
            appointmentDateTime,
            doctorName
        );

        await _notificationRepository.AddAsync(notification);
        await SendNotificationAsync(notification);
    }

    public async Task SendAppointmentCancellationAsync(
        string recipientEmail,
        string recipientName,
        DateTime appointmentDateTime,
        string doctorName,
        string cancellationReason)
    {
        var notification = Notification.CreateAppointmentCancellation(
            recipientEmail,
            recipientName,
            appointmentDateTime,
            doctorName,
            cancellationReason
        );

        await _notificationRepository.AddAsync(notification);
        await SendNotificationAsync(notification);
    }

    public async Task SendAppointmentReminderAsync(
        string recipientEmail,
        string recipientName,
        DateTime appointmentDateTime,
        string doctorName)
    {
        var notification = Notification.CreateAppointmentReminder(
            recipientEmail,
            recipientName,
            appointmentDateTime,
            doctorName
        );

        await _notificationRepository.AddAsync(notification);
        await SendNotificationAsync(notification);
    }

    public async Task ResendFailedNotificationsAsync()
    {
        var failedNotifications = await _notificationRepository.GetFailedNotificationsAsync();
        foreach (var notification in failedNotifications)
        {
            await SendNotificationAsync(notification);
        }
    }

    private async Task SendNotificationAsync(Notification notification)
    {
        try
        {
            // Log the notification
            var formattedMessage = $"""
                ==============================
                
                Receiver: {notification.Recipient.Email}
                Subject: {notification.Content.Subject}
                
                Message:
                {notification.Content.Body}
                
                ==============================
                """;
            _logger.LogInformation(formattedMessage);

            // Mark as sent
            notification.MarkAsSent();
            await _notificationRepository.UpdateAsync(notification);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send notification {NotificationId}", notification.Id.Value);
            notification.MarkAsFailed(ex.Message);
            await _notificationRepository.UpdateAsync(notification);
        }
    }
} 