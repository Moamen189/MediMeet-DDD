using DoctorAvailability.Business.Services;
using DoctorAvailability.Shared.Contracts;

namespace DoctorAvailability.Api.DoctorAvailabilityShared;

public class DoctorAvailabilityApi: IDoctorAvailabilityApi
{
    private readonly SlotServices _slotServices;

    public DoctorAvailabilityApi(SlotServices slotServices)
    {
        _slotServices = slotServices;
    }
    public bool ReserveSlot(Guid slotId)
    {
       return _slotServices.ReserveSlot(slotId);
    }
}