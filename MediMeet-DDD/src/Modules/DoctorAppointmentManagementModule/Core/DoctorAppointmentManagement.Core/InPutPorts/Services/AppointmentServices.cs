using DoctorAppointmentManagement.Core.Dtos;
using DoctorAppointmentManagement.Core.Models;
using DoctorAppointmentManagement.Core.OutputPorts.IRepository;

namespace DoctorAppointmentManagement.Core.InPutPorts.Services;

public class AppointmentServices : IAppointmentServices
{
	private readonly IAppointmentRepository _appointmentRepository;

	public AppointmentServices(IAppointmentRepository appointmentRepository)
	{
		_appointmentRepository = appointmentRepository;
	}

	public async Task<List<AppointmentConfirmationDto>> GetUpcomingAppointments()
	{
		return await _appointmentRepository.GetUpComingAppointments();
	}
	public async Task<AppointmentConfirmation> CompleteUpcomingAppointment(AppointmentConfirmationDto appointmentConfirmationDto)
	{
		return await _appointmentRepository.CompleteUpComingAppointment(appointmentConfirmationDto);
	}
	public async Task<AppointmentConfirmation> CancelUpcomingAppointment(AppointmentConfirmationDto appointmentConfirmationDto)
	{
		return await _appointmentRepository.CancelUpComingAppointment(appointmentConfirmationDto);
	}


}