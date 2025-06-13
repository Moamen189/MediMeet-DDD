namespace DoctorAvailability.Shared.Contracts;

public interface IDoctorAvailabilityApi
{
    public bool ReserveSlot(Guid slotId);
}