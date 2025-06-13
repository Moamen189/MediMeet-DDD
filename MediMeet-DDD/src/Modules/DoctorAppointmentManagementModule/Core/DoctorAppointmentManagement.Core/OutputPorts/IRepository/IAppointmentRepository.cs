using DoctorAppointmentManagement.Core.Dtos;
using DoctorAppointmentManagement.Core.Models;

namespace DoctorAppointmentManagement.Core.OutputPorts.IRepository;

public interface IAppointmentRepository
{
	Task<List<AppointmentConfirmationDto>> GetUpComingAppointments();

	Task<AppointmentConfirmation> CompleteUpComingAppointment(AppointmentConfirmationDto appointmentConfirmationDto);
	Task<AppointmentConfirmation> CancelUpComingAppointment(AppointmentConfirmationDto appointmentConfirmationDto);
}