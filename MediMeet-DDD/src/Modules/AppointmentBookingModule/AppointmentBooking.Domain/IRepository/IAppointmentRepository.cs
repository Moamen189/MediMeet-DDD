using AppointmentBooking.Domain.DomainModels;

namespace AppointmentBooking.Domain.IRepository;

public interface IAppointmentRepository
{
	Task<Appointment> CreateAsync(Appointment appointment);
	Task<List<Appointment>> GetUpcomingAppointmentsAsync();
	Task<Appointment> GetByIdAsync(AppointmentId id);
	Task<bool> UpdateAsync(Appointment appointment);
	
	// Additional DDD-focused methods
	Task<List<Appointment>> GetPatientAppointmentsAsync(PatientId patientId);
	Task<bool> ExistsOverlappingAppointmentAsync(PatientId patientId, AppointmentDateTime dateTime);
	Task<int> GetPatientAppointmentCountInPeriodAsync(PatientId patientId, DateTime startDate, DateTime endDate);
	Task<List<Appointment>> GetAppointmentsForDoctorAsync(DoctorName doctorName, DateTime date);
}