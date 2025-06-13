using Shared.RootEntity;

namespace DoctorAvailability.Business.DomainModels;

public class TimeSlot : Entity
{
    private TimeSlot() { } // For EF Core

    private TimeSlot(
        Guid id,
        DoctorId doctorId,
        TimeRange timeRange,
        Money consultationFee)
    {
        Id = id;
        DoctorId = doctorId;
        TimeRange = timeRange;
        ConsultationFee = consultationFee;
        IsReserved = false;
    }

    public Guid Id { get; private set; }
    public DoctorId DoctorId { get; private set; }
    public TimeRange TimeRange { get; private set; }
    public Money ConsultationFee { get; private set; }
    public bool IsReserved { get; private set; }

    public static TimeSlot Create(
        Guid id,
        DoctorId doctorId,
        DateTime startTime,
        DateTime endTime,
        Money consultationFee)
    {
        var timeRange = TimeRange.Create(startTime, endTime);
        return new TimeSlot(id, doctorId, timeRange, consultationFee);
    }

    public void UpdateFee(Money newFee)
    {
        if (IsReserved)
        {
            throw new DomainException("Cannot update fee for a reserved slot");
        }
        ConsultationFee = newFee;
    }

    public void Reserve()
    {
        if (IsReserved)
        {
            throw new DomainException("Slot is already reserved");
        }

        if (TimeRange.Start <= DateTime.UtcNow)
        {
            throw new DomainException("Cannot reserve slots in the past");
        }

        IsReserved = true;
    }

    public void CancelReservation()
    {
        if (!IsReserved)
        {
            throw new DomainException("Slot is not reserved");
        }

        if (TimeRange.Start <= DateTime.UtcNow.AddHours(2))
        {
            throw new DomainException("Cannot cancel reservation less than 2 hours before the appointment");
        }

        IsReserved = false;
    }

    public bool OverlapsWith(TimeSlot other)
    {
        return DoctorId.Equals(other.DoctorId) && TimeRange.OverlapsWith(other.TimeRange);
    }
} 