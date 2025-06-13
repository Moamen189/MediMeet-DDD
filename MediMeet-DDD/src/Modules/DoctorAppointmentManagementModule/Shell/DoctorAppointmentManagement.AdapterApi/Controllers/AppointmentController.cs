using DoctorAppointmentManagement.Core.Dtos;
using DoctorAppointmentManagement.Core.InPutPorts.Services;
using DoctorAppointmentManagement.Core.Models;
using Microsoft.AspNetCore.Mvc;


namespace DoctorAppointmentManagement.AdapterApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AppointmentController : ControllerBase
{
	private readonly IAppointmentServices _appointmentServices;

	public AppointmentController(IAppointmentServices appointmentServices)
	{
		_appointmentServices = appointmentServices;
	}

	[HttpGet("GetUpcomingAppointments")]
	public async Task<IActionResult> GetUpcomingAppointments()
	{

		return Ok(await _appointmentServices.GetUpcomingAppointments());
	}

	[HttpPost("CompleteUpcomingAppointment")]
	public async Task<IActionResult> CompleteUpcomingAppointment(AppointmentConfirmationDto appointmentConfirmationDto)
	{
		return Ok(await _appointmentServices.CompleteUpcomingAppointment(appointmentConfirmationDto));
	}

	[HttpPost("CancelUpcomingAppointment")]
	public async Task<IActionResult> CancelUpcomingAppointment(AppointmentConfirmationDto appointmentConfirmationDto)
	{
		return Ok(await _appointmentServices.CancelUpcomingAppointment(appointmentConfirmationDto));
	}
}