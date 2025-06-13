using AppointmentBooking.Domain.DomainModels;

namespace AppointmentBooking.Domain.IRepository;

public interface IAppointmentRepository
{
	Task<Appointment> CreateAsync(Appointment appointment);
	Task<List<Appointment>> GetUpcomingAppointmentsAsync();
	Task<Appointment> GetByIdAsync(AppointmentId id);
	Task<bool> UpdateAsync(Appointment appointment);
}