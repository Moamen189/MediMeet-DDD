using AppointmentBooking.Domain.IRepository;
using DoctorAppointmentManagement.Shared.IntegrationEvents;
using Shared.EventBus;

namespace AppointmentBooking.Application.EventHandlers;

public class AppointmentCanceledEventHandler:IIntegrationEventHandler<AppointmentCanceledEvent>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentCanceledEventHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }
    public Task Handle(AppointmentCanceledEvent notification, CancellationToken cancellationToken)
    {
        _appointmentRepository.CancelAppointment(notification.appointmentId);
        return Task.CompletedTask;
    }
}