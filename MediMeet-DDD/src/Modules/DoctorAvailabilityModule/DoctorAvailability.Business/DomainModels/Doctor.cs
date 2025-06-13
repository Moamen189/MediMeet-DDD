using Shared.RootEntity;

namespace DoctorAvailability.Business.DomainModels;

public class Doctor : AggregateRoot
{
    private readonly List<TimeSlot> _availableSlots;
    
    private Doctor()
    {
        _availableSlots = new List<TimeSlot>();
    }

    private Doctor(DoctorId id, DoctorName name, Specialty specialty)
    {
        Id = id;
        Name = name;
        Specialty = specialty;
        _availableSlots = new List<TimeSlot>();
    }

    public DoctorId Id { get; private set; }
    public DoctorName Name { get; private set; }
    public Specialty Specialty { get; private set; }
    public IReadOnlyList<TimeSlot> AvailableSlots => _availableSlots.AsReadOnly();

    public static Doctor Create(int id, string name, string specialty)
    {
        return new Doctor(
            DoctorId.From(id),
            DoctorName.From(name),
            Specialty.From(specialty)
        );
    }

    public void AddAvailabilitySlot(DateTime startTime, DateTime endTime, Money consultationFee)
    {
        if (startTime >= endTime)
        {
            throw new DomainException("End time must be after start time");
        }

        if (startTime < DateTime.UtcNow)
        {
            throw new DomainException("Cannot add availability slots in the past");
        }

        var newSlot = TimeSlot.Create(
            Guid.NewGuid(),
            this.Id,
            startTime,
            endTime,
            consultationFee
        );

        if (_availableSlots.Any(slot => slot.OverlapsWith(newSlot)))
        {
            throw new DomainException("New slot overlaps with existing slots");
        }

        _availableSlots.Add(newSlot);
        AddDomainEvent(new AvailabilitySlotAddedDomainEvent(this, newSlot));
    }

    public void RemoveAvailabilitySlot(Guid slotId)
    {
        var slot = _availableSlots.FirstOrDefault(s => s.Id == slotId);
        if (slot == null)
        {
            throw new DomainException($"Slot with ID {slotId} not found");
        }

        if (slot.IsReserved)
        {
            throw new DomainException("Cannot remove a reserved slot");
        }

        _availableSlots.Remove(slot);
        AddDomainEvent(new AvailabilitySlotRemovedDomainEvent(this, slot));
    }

    public void UpdateConsultationFee(Guid slotId, Money newFee)
    {
        var slot = _availableSlots.FirstOrDefault(s => s.Id == slotId);
        if (slot == null)
        {
            throw new DomainException($"Slot with ID {slotId} not found");
        }

        if (slot.IsReserved)
        {
            throw new DomainException("Cannot update fee for a reserved slot");
        }

        slot.UpdateFee(newFee);
        AddDomainEvent(new SlotFeeUpdatedDomainEvent(this, slot));
    }
} 