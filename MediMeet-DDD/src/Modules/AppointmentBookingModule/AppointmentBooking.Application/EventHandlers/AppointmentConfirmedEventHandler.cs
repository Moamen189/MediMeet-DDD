using AppointmentBooking.Domain.IRepository;
using DoctorAppointmentManagement.Shared.IntegrationEvents;
using Shared.EventBus;

namespace AppointmentBooking.Application.EventHandlers;

public class AppointmentConfirmedEventHandler:IIntegrationEventHandler<AppointmentConfirmedEvent>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentConfirmedEventHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public Task Handle(AppointmentConfirmedEvent notification, CancellationToken cancellationToken)
    {
        _appointmentRepository.ConfirmAppointment(notification.appointmentId);
        return Task.CompletedTask;
    }
}