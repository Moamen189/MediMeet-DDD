using AppointmentBooking.Domain.DomainModels;
using Shared.EventBus;

namespace AppointmentBooking.Domain.DomainEvents;

public record AppointmentRescheduledDomainEvent : IDomainEvent
{
    public AppointmentRescheduledDomainEvent(
        Appointment appointment,
        AppointmentDateTime oldDateTime)
    {
        AppointmentId = appointment.Id;
        PatientName = appointment.PatientName;
        DoctorName = appointment.DoctorName;
        OldDateTime = oldDateTime;
        NewDateTime = appointment.ReservedAt;
        SlotId = appointment.SlotId;
    }

    public AppointmentId AppointmentId { get; }
    public PatientName PatientName { get; }
    public DoctorName DoctorName { get; }
    public AppointmentDateTime OldDateTime { get; }
    public AppointmentDateTime NewDateTime { get; }
    public Guid SlotId { get; }
} 