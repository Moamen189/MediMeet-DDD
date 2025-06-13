using Shared.EventBus;

namespace DoctorAppointmentManagement.Shared.IntegrationEvents;

public record AppointmentCanceledEvent(Guid appointmentId , Guid slotId):IIntegrationEvent
{
    
}