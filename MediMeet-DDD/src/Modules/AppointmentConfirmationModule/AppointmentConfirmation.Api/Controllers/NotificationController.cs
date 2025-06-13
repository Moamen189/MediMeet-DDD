using AppointmentConfirmation.Api.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentConfirmation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly NotificationService _notificationService;

    public NotificationController(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("appointment/confirmation")]
    public async Task<IActionResult> SendAppointmentConfirmation([FromBody] SendAppointmentConfirmationRequest request)
    {
        try
        {
            await _notificationService.SendAppointmentConfirmationAsync(
                request.RecipientEmail,
                request.RecipientName,
                request.AppointmentDateTime,
                request.DoctorName
            );
            return Ok();
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("appointment/cancellation")]
    public async Task<IActionResult> SendAppointmentCancellation([FromBody] SendAppointmentCancellationRequest request)
    {
        try
        {
            await _notificationService.SendAppointmentCancellationAsync(
                request.RecipientEmail,
                request.RecipientName,
                request.AppointmentDateTime,
                request.DoctorName,
                request.CancellationReason
            );
            return Ok();
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("appointment/reminder")]
    public async Task<IActionResult> SendAppointmentReminder([FromBody] SendAppointmentReminderRequest request)
    {
        try
        {
            await _notificationService.SendAppointmentReminderAsync(
                request.RecipientEmail,
                request.RecipientName,
                request.AppointmentDateTime,
                request.DoctorName
            );
            return Ok();
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("resend-failed")]
    public async Task<IActionResult> ResendFailedNotifications()
    {
        try
        {
            await _notificationService.ResendFailedNotificationsAsync();
            return Ok();
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class SendAppointmentConfirmationRequest
{
    public string RecipientEmail { get; set; }
    public string RecipientName { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public string DoctorName { get; set; }
}

public class SendAppointmentCancellationRequest
{
    public string RecipientEmail { get; set; }
    public string RecipientName { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public string DoctorName { get; set; }
    public string CancellationReason { get; set; }
}

public class SendAppointmentReminderRequest
{
    public string RecipientEmail { get; set; }
    public string RecipientName { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public string DoctorName { get; set; }
} 