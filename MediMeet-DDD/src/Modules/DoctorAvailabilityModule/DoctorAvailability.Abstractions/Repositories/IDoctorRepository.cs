using DoctorAvailability.Abstractions.Models;

namespace DoctorAvailability.Abstractions.Repositories
{
    public interface IDoctorRepository
    {
        Task<Doctor> GetByIdAsync(DoctorId id);
        Task<List<Doctor>> GetAllAsync();
        Task<List<Doctor>> GetDoctorsBySpecialtyAsync(Specialty specialty);
        Task<List<TimeSlot>> GetAvailableSlotsAsync(DoctorId doctorId, DateTime startDate, DateTime endDate);
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
    }
} 