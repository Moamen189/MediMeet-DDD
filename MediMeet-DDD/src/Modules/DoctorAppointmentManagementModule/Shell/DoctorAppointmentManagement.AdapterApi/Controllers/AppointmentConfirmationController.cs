using DoctorAppointmentManagement.Core.Application.Dtos;
using DoctorAppointmentManagement.Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentManagement.AdapterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentConfirmationController : ControllerBase
{
    private readonly AppointmentConfirmationService _appointmentService;

    public AppointmentConfirmationController(AppointmentConfirmationService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentConfirmationDto>> CreateAppointmentConfirmation(
        [FromBody] CreateAppointmentConfirmationDto request)
    {
        try
        {
            var result = await _appointmentService.CreateAppointmentConfirmationAsync(request);
            return Ok(result);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{appointmentId}/confirm")]
    public async Task<ActionResult<AppointmentConfirmationDto>> ConfirmAppointment(
        Guid appointmentId,
        [FromBody] UpdateAppointmentStatusDto request)
    {
        try
        {
            var result = await _appointmentService.ConfirmAppointmentAsync(
                appointmentId,
                request.Comments
            );
            return Ok(result);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{appointmentId}/cancel")]
    public async Task<ActionResult<AppointmentConfirmationDto>> CancelAppointment(
        Guid appointmentId,
        [FromBody] UpdateAppointmentStatusDto request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Comments))
            {
                return BadRequest("Cancellation reason is required");
            }

            var result = await _appointmentService.CancelAppointmentAsync(
                appointmentId,
                request.Comments
            );
            return Ok(result);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("upcoming")]
    public async Task<ActionResult<List<AppointmentConfirmationDto>>> GetUpcomingAppointments()
    {
        try
        {
            var result = await _appointmentService.GetUpcomingAppointmentsAsync();
            return Ok(result);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("patient/{patientId}")]
    public async Task<ActionResult<List<AppointmentConfirmationDto>>> GetAppointmentsByPatient(int patientId)
    {
        try
        {
            var result = await _appointmentService.GetAppointmentsByPatientAsync(patientId);
            return Ok(result);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }
} 