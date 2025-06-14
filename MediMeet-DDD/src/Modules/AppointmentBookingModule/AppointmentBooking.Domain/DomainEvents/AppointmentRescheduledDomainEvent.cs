using AppointmentBooking.Domain.DomainModels;

namespace AppointmentBooking.Domain.DomainEvents;

public record AppointmentRescheduledDomainEvent
{
    public AppointmentId AppointmentId { get; }
    public Guid SlotId { get; }
    public PatientId PatientId { get; }
    public PatientName PatientName { get; }
    public DoctorName DoctorName { get; }
    public AppointmentDateTime OldDateTime { get; }
    public AppointmentDateTime NewDateTime { get; }

    public AppointmentRescheduledDomainEvent(Appointment appointment, AppointmentDateTime oldDateTime)
    {
        AppointmentId = appointment.Id;
        SlotId = appointment.SlotId;
        PatientId = appointment.PatientId;
        PatientName = appointment.PatientName;
        DoctorName = appointment.DoctorName;
        OldDateTime = oldDateTime;
        NewDateTime = appointment.ReservedAt;
    }
} 