using Shared.EventBus;

namespace DoctorAppointmentManagement.Shared.IntegrationEvents;

public record AppointmentConfirmedEvent(Guid appointmentId) : IIntegrationEvent
{
    
}
