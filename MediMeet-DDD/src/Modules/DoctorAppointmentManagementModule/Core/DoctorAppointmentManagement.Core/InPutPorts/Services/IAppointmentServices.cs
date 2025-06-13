using DoctorAppointmentManagement.Core.Dtos;
using DoctorAppointmentManagement.Core.Models;

namespace DoctorAppointmentManagement.Core.InPutPorts.Services;

public interface IAppointmentServices
{
	Task<List<AppointmentConfirmationDto>> GetUpcomingAppointments();
	Task<AppointmentConfirmation> CompleteUpcomingAppointment(AppointmentConfirmationDto appointmentConfirmationDto);
	Task<AppointmentConfirmation> CancelUpcomingAppointment(AppointmentConfirmationDto appointmentConfirmationDto);

}