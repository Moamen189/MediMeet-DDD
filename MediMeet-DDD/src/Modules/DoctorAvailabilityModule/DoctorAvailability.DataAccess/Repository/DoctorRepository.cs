using DoctorAvailability.Business.DomainModels;
using DoctorAvailability.DataAccess.Database;
using DoctorAvailability.DataAccess.IRepository;
using MediatR;

namespace DoctorAvailability.DataAccess.Repository;

public class DoctorRepository : IDoctorRepository
{
    private readonly IMediator _mediator;

    public DoctorRepository(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Doctor> GetByIdAsync(DoctorId id)
    {
        var doctorEntity = InMemoryDb.Doctors.FirstOrDefault(d => d.Id == id.Value);
        if (doctorEntity == null)
        {
            throw new DomainException($"Doctor with ID {id.Value} not found");
        }

        var doctor = Doctor.Create(
            doctorEntity.Id,
            doctorEntity.Name,
            doctorEntity.Specialty ?? "General"
        );

        // Load doctor's slots
        var slots = InMemoryDb.Slots
            .Where(s => s.DoctorId == id.Value)
            .Select(s => TimeSlot.Create(
                s.Id,
                id,
                s.Time,
                s.Time.AddHours(1), // Assuming 1-hour slots
                Money.From(s.Cost)
            ))
            .ToList();

        foreach (var slot in slots)
        {
            if (slot.IsReserved)
            {
                slot.Reserve();
            }
        }

        return doctor;
    }

    public Task<List<Doctor>> GetAllAsync()
    {
        var doctors = InMemoryDb.Doctors
            .Select(d => Doctor.Create(
                d.Id,
                d.Name,
                d.Specialty ?? "General"
            ))
            .ToList();

        return Task.FromResult(doctors);
    }

    public async Task<Doctor> AddAsync(Doctor doctor)
    {
        var existingDoctor = InMemoryDb.Doctors.FirstOrDefault(d => d.Id == doctor.Id.Value);
        if (existingDoctor != null)
        {
            throw new DomainException($"Doctor with ID {doctor.Id.Value} already exists");
        }

        var doctorEntity = new Entities.Doctor
        {
            Id = doctor.Id.Value,
            Name = doctor.Name.Value,
            Specialty = doctor.Specialty.Value
        };

        InMemoryDb.Doctors.Add(doctorEntity);

        foreach (var domainEvent in doctor.DomainEvents)
        {
            await _mediator.Publish(domainEvent);
        }

        return doctor;
    }

    public async Task UpdateAsync(Doctor doctor)
    {
        var existingDoctor = InMemoryDb.Doctors.FirstOrDefault(d => d.Id == doctor.Id.Value);
        if (existingDoctor == null)
        {
            throw new DomainException($"Doctor with ID {doctor.Id.Value} not found");
        }

        existingDoctor.Name = doctor.Name.Value;
        existingDoctor.Specialty = doctor.Specialty.Value;

        // Update slots
        var existingSlots = InMemoryDb.Slots.Where(s => s.DoctorId == doctor.Id.Value).ToList();
        foreach (var slot in doctor.AvailableSlots)
        {
            var existingSlot = existingSlots.FirstOrDefault(s => s.Id == slot.Id);
            if (existingSlot != null)
            {
                existingSlot.Time = slot.TimeRange.Start;
                existingSlot.Cost = slot.ConsultationFee.Value;
                existingSlot.IsReserved = slot.IsReserved;
            }
            else
            {
                InMemoryDb.Slots.Add(new Entities.Slot
                {
                    Id = slot.Id,
                    DoctorId = doctor.Id.Value,
                    DoctorName = doctor.Name.Value,
                    Time = slot.TimeRange.Start,
                    Cost = slot.ConsultationFee.Value,
                    IsReserved = slot.IsReserved
                });
            }
        }

        foreach (var domainEvent in doctor.DomainEvents)
        {
            await _mediator.Publish(domainEvent);
        }
    }

    public Task<List<Doctor>> GetDoctorsBySpecialtyAsync(Specialty specialty)
    {
        var doctors = InMemoryDb.Doctors
            .Where(d => d.Specialty == specialty.Value)
            .Select(d => Doctor.Create(
                d.Id,
                d.Name,
                d.Specialty ?? "General"
            ))
            .ToList();

        return Task.FromResult(doctors);
    }

    public Task<List<TimeSlot>> GetAvailableSlotsAsync(DoctorId doctorId, DateTime startDate, DateTime endDate)
    {
        var slots = InMemoryDb.Slots
            .Where(s => 
                s.DoctorId == doctorId.Value &&
                !s.IsReserved &&
                s.Time >= startDate &&
                s.Time <= endDate)
            .Select(s => TimeSlot.Create(
                s.Id,
                doctorId,
                s.Time,
                s.Time.AddHours(1), // Assuming 1-hour slots
                Money.From(s.Cost)
            ))
            .ToList();

        return Task.FromResult(slots);
    }

    public Task<bool> HasOverlappingSlots(DoctorId doctorId, DateTime startTime, DateTime endTime)
    {
        var hasOverlap = InMemoryDb.Slots.Any(s =>
            s.DoctorId == doctorId.Value &&
            s.Time < endTime &&
            s.Time.AddHours(1) > startTime); // Assuming 1-hour slots

        return Task.FromResult(hasOverlap);
    }
}