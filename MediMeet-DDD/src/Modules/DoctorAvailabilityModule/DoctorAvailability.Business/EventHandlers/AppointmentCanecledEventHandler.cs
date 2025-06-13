using DoctorAppointmentManagement.Shared.IntegrationEvents;
using DoctorAvailability.DataAccess.IRepository;
using Shared.EventBus;

namespace DoctorAvailability.Business.EventHandlers;

public class AppointmentCanceledEventHandler:IIntegrationEventHandler<AppointmentCanceledEvent>
{
    private readonly ISlotRepository _slotRepository;

    public AppointmentCanceledEventHandler(ISlotRepository slotRepository)
    {
        _slotRepository = slotRepository;
    }
    public Task Handle(AppointmentCanceledEvent notification, CancellationToken cancellationToken)
    {
         _slotRepository.DeleteById(notification.slotId);
         return Task.CompletedTask;
    }
}