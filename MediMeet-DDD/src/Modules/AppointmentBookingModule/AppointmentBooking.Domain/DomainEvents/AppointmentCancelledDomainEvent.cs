using AppointmentBooking.Domain.DomainModels;

namespace AppointmentBooking.Domain.DomainEvents;

public record AppointmentCancelledDomainEvent
{
    public AppointmentId AppointmentId { get; }
    public Guid SlotId { get; }
    public PatientId PatientId { get; }
    public PatientName PatientName { get; }
    public DoctorName DoctorName { get; }
    public AppointmentDateTime ReservedAt { get; }

    public AppointmentCancelledDomainEvent(Appointment appointment)
    {
        AppointmentId = appointment.Id;
        SlotId = appointment.SlotId;
        PatientId = appointment.PatientId;
        PatientName = appointment.PatientName;
        DoctorName = appointment.DoctorName;
        ReservedAt = appointment.ReservedAt;
    }
} 