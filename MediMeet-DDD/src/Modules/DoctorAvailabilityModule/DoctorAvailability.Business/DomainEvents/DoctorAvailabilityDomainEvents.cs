using DoctorAvailability.Business.DomainModels;
using Shared.EventBus;

namespace DoctorAvailability.Business.DomainEvents;

public record AvailabilitySlotAddedDomainEvent : IDomainEvent
{
    public AvailabilitySlotAddedDomainEvent(Doctor doctor, TimeSlot slot)
    {
        DoctorId = doctor.Id;
        DoctorName = doctor.Name;
        SlotId = slot.Id;
        StartTime = slot.TimeRange.Start;
        EndTime = slot.TimeRange.End;
        ConsultationFee = slot.ConsultationFee;
    }

    public DoctorId DoctorId { get; }
    public DoctorName DoctorName { get; }
    public Guid SlotId { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public Money ConsultationFee { get; }
}

public record AvailabilitySlotRemovedDomainEvent : IDomainEvent
{
    public AvailabilitySlotRemovedDomainEvent(Doctor doctor, TimeSlot slot)
    {
        DoctorId = doctor.Id;
        DoctorName = doctor.Name;
        SlotId = slot.Id;
        StartTime = slot.TimeRange.Start;
        EndTime = slot.TimeRange.End;
    }

    public DoctorId DoctorId { get; }
    public DoctorName DoctorName { get; }
    public Guid SlotId { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
}

public record SlotFeeUpdatedDomainEvent : IDomainEvent
{
    public SlotFeeUpdatedDomainEvent(Doctor doctor, TimeSlot slot)
    {
        DoctorId = doctor.Id;
        DoctorName = doctor.Name;
        SlotId = slot.Id;
        NewFee = slot.ConsultationFee;
    }

    public DoctorId DoctorId { get; }
    public DoctorName DoctorName { get; }
    public Guid SlotId { get; }
    public Money NewFee { get; }
}

public record SlotReservedDomainEvent : IDomainEvent
{
    public SlotReservedDomainEvent(TimeSlot slot)
    {
        DoctorId = slot.DoctorId;
        SlotId = slot.Id;
        StartTime = slot.TimeRange.Start;
        EndTime = slot.TimeRange.End;
    }

    public DoctorId DoctorId { get; }
    public Guid SlotId { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
}

public record SlotReservationCancelledDomainEvent : IDomainEvent
{
    public SlotReservationCancelledDomainEvent(TimeSlot slot)
    {
        DoctorId = slot.DoctorId;
        SlotId = slot.Id;
        StartTime = slot.TimeRange.Start;
        EndTime = slot.TimeRange.End;
    }

    public DoctorId DoctorId { get; }
    public Guid SlotId { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
} 