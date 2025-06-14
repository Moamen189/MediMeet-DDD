using DoctorAvailability.Abstractions.Models;
using DoctorAvailability.Abstractions.Repositories;
using MediatR;

namespace DoctorAvailability.Business.Services;

public class DoctorAvailabilityService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMediator _mediator;

    public DoctorAvailabilityService(
        IDoctorRepository doctorRepository,
        IMediator mediator)
    {
        _doctorRepository = doctorRepository;
        _mediator = mediator;
    }

    public async Task<Doctor> GetDoctorAsync(int doctorId)
    {
        return await _doctorRepository.GetByIdAsync(DoctorId.From(doctorId));
    }

    public async Task<List<Doctor>> GetDoctorsBySpecialtyAsync(Specialty specialty)
    {
        return await _doctorRepository.GetDoctorsBySpecialtyAsync(specialty);
    }

    public async Task<List<TimeSlot>> GetAvailableSlotsAsync(DoctorId doctorId, DateTime startDate, DateTime endDate)
    {
        return await _doctorRepository.GetAvailableSlotsAsync(doctorId, startDate, endDate);
    }

    public async Task AddAvailabilitySlotAsync(
        int doctorId,
        DateTime startTime,
        DateTime endTime,
        decimal consultationFee)
    {
        var doctor = await _doctorRepository.GetByIdAsync(DoctorId.From(doctorId));

        var hasOverlap = await _doctorRepository.HasOverlappingSlots(
            doctor.Id,
            startTime,
            endTime
        );

        if (hasOverlap)
        {
            throw new DomainException("The new slot overlaps with existing slots");
        }

        doctor.AddAvailabilitySlot(
            startTime,
            endTime,
            Money.From(consultationFee)
        );

        await _doctorRepository.UpdateAsync(doctor);
    }

    public async Task RemoveAvailabilitySlotAsync(int doctorId, Guid slotId)
    {
        var doctor = await _doctorRepository.GetByIdAsync(DoctorId.From(doctorId));
        doctor.RemoveAvailabilitySlot(slotId);
        await _doctorRepository.UpdateAsync(doctor);
    }

    public async Task UpdateSlotFeeAsync(int doctorId, Guid slotId, decimal newFee)
    {
        var doctor = await _doctorRepository.GetByIdAsync(DoctorId.From(doctorId));
        doctor.UpdateConsultationFee(slotId, Money.From(newFee));
        await _doctorRepository.UpdateAsync(doctor);
    }

    public async Task<bool> ReserveSlotAsync(int doctorId, Guid slotId)
    {
        var doctor = await _doctorRepository.GetByIdAsync(DoctorId.From(doctorId));
        var slot = doctor.AvailableSlots.FirstOrDefault(s => s.Id == slotId);
        
        if (slot == null)
        {
            throw new DomainException($"Slot {slotId} not found for doctor {doctorId}");
        }

        try
        {
            slot.Reserve();
            await _doctorRepository.UpdateAsync(doctor);
            await _mediator.Publish(new SlotReservedDomainEvent(slot));
            return true;
        }
        catch (DomainException)
        {
            return false;
        }
    }

    public async Task<bool> CancelReservationAsync(int doctorId, Guid slotId)
    {
        var doctor = await _doctorRepository.GetByIdAsync(DoctorId.From(doctorId));
        var slot = doctor.AvailableSlots.FirstOrDefault(s => s.Id == slotId);
        
        if (slot == null)
        {
            throw new DomainException($"Slot {slotId} not found for doctor {doctorId}");
        }

        try
        {
            slot.CancelReservation();
            await _doctorRepository.UpdateAsync(doctor);
            await _mediator.Publish(new SlotReservationCancelledDomainEvent(slot));
            return true;
        }
        catch (DomainException)
        {
            return false;
        }
    }

    public async Task AddDoctorAsync(string name, Specialty specialty)
    {
        var doctor = Doctor.Create(DoctorId.New(), name, specialty);
        await _doctorRepository.AddAsync(doctor);
    }

    public async Task AddTimeSlotAsync(DoctorId doctorId, DateTime startTime, DateTime endTime)
    {
        var doctor = await _doctorRepository.GetByIdAsync(doctorId);
        var slot = TimeSlot.Create(startTime, endTime);
        doctor.AddTimeSlot(slot);
        await _doctorRepository.UpdateAsync(doctor);
    }
} 