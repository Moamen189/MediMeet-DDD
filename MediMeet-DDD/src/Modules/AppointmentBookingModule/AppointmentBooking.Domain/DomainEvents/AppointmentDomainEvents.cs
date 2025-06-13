using AppointmentBooking.Domain.DomainModels;
using Shared.EventBus;

namespace AppointmentBooking.Domain.DomainEvents;

public record AppointmentCreatedDomainEvent : IDomainEvent
{
    public AppointmentCreatedDomainEvent(Appointment appointment)
    {
        AppointmentId = appointment.Id;
        PatientName = appointment.PatientName;
        DoctorName = appointment.DoctorName;
        ReservedAt = appointment.ReservedAt;
        SlotId = appointment.SlotId;
    }

    public AppointmentId AppointmentId { get; }
    public PatientName PatientName { get; }
    public DoctorName DoctorName { get; }
    public AppointmentDateTime ReservedAt { get; }
    public Guid SlotId { get; }
}

public record AppointmentCancelledDomainEvent : IDomainEvent
{
    public AppointmentCancelledDomainEvent(Appointment appointment)
    {
        AppointmentId = appointment.Id;
        PatientName = appointment.PatientName;
        DoctorName = appointment.DoctorName;
        ReservedAt = appointment.ReservedAt;
        SlotId = appointment.SlotId;
    }

    public AppointmentId AppointmentId { get; }
    public PatientName PatientName { get; }
    public DoctorName DoctorName { get; }
    public AppointmentDateTime ReservedAt { get; }
    public Guid SlotId { get; }
}

public record AppointmentConfirmedDomainEvent : IDomainEvent
{
    public AppointmentConfirmedDomainEvent(Appointment appointment)
    {
        AppointmentId = appointment.Id;
        PatientName = appointment.PatientName;
        DoctorName = appointment.DoctorName;
        ReservedAt = appointment.ReservedAt;
        SlotId = appointment.SlotId;
    }

    public AppointmentId AppointmentId { get; }
    public PatientName PatientName { get; }
    public DoctorName DoctorName { get; }
    public AppointmentDateTime ReservedAt { get; }
    public Guid SlotId { get; }
} 