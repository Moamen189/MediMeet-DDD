using AppointmentBooking.Domain.DomainEvents;
using AppointmentConfirmation.Shared;
using DoctorAvailability.Shared.Contracts;
using Shared.EventBus;

namespace AppointmentBooking.Application.EventHandlers;

public class CreateAppointmentDomainEventHandler : IDomainEventHandler<CreateAppointmentDomainEvent>
{
    private readonly INotificationApi _notificationApi;
    private readonly IDoctorAvailabilityApi _doctorAvailabilityApi;

    public CreateAppointmentDomainEventHandler(INotificationApi notificationApi,IDoctorAvailabilityApi doctorAvailabilityApi)
    {
        _notificationApi = notificationApi;
        _doctorAvailabilityApi = doctorAvailabilityApi;
    }

    public Task Handle(CreateAppointmentDomainEvent notification, CancellationToken cancellationToken)
    {
        // Send notification to patient and doctor
        var message =
            $"Appointment created for {notification.patientName} on {notification.reservedAt} with Dr. {notification.doctorName}";
        var sendTo = new List<string>
        {
            notification.patientName, notification.doctorName
        };
        _notificationApi.SendNotification(sendTo, message);
        // update the slot as reserved
        _doctorAvailabilityApi.ReserveSlot( notification.slotId);
        return Task.CompletedTask;
    }
}