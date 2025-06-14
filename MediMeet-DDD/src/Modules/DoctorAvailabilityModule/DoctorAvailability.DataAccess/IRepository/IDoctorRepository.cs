using DoctorAvailability.DataAccess.Entities;

namespace DoctorAvailability.DataAccess.IRepository;

public interface IDoctorRepository
{
	Task<Doctor> GetByIdAsync(DoctorId id);
	Task<List<Doctor>> GetAllAsync();
	Task<Doctor> AddAsync(Doctor doctor);
	Task UpdateAsync(Doctor doctor);
	Task<List<Doctor>> GetDoctorsBySpecialtyAsync(Specialty specialty);
	Task<List<TimeSlot>> GetAvailableSlotsAsync(Doctor doctorId, DateTime startDate, DateTime endDate);
	Task<bool> HasOverlappingSlots(DoctorId doctorId, DateTime startTime, DateTime endTime);
}