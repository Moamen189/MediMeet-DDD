using DoctorAvailability.Abstractions.Models;
using DoctorAvailability.Abstractions.Repositories;

namespace DoctorAvailability.Business.Services;

public class SlotServices
{
    private readonly IDoctorRepository _doctorRepository;

    public SlotServices(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<List<TimeSlot>> GetAvailableSlotsAsync(DoctorId doctorId, DateTime startDate, DateTime endDate)
    {
        return await _doctorRepository.GetAvailableSlotsAsync(doctorId, startDate, endDate);
    }

    public async Task AddTimeSlotAsync(DoctorId doctorId, DateTime startTime, DateTime endTime)
    {
        var doctor = await _doctorRepository.GetByIdAsync(doctorId);
        var slot = TimeSlot.Create(startTime, endTime);
        doctor.AddTimeSlot(slot);
        await _doctorRepository.UpdateAsync(doctor);
    }
}