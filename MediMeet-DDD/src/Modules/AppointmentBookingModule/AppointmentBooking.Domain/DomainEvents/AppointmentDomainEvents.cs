using Shared.RootEntity;

namespace AppointmentBooking.Domain.DomainEvents
{
    public record AppointmentCreatedDomainEvent(
        string AppointmentId,
        string PatientId,
        string PatientName,
        string DoctorId,
        string DoctorName,
        DateTime ReservedAt) : IDomainEvent;

    public record AppointmentCancelledDomainEvent(
        string AppointmentId,
        string PatientId,
        string PatientName,
        string DoctorId,
        string DoctorName,
        DateTime ReservedAt,
        string CancellationReason) : IDomainEvent;

    public record AppointmentConfirmedDomainEvent(
        string AppointmentId,
        string PatientId,
        string PatientName,
        string DoctorId,
        string DoctorName,
        DateTime ReservedAt) : IDomainEvent;

    public record AppointmentRescheduledDomainEvent(
        string AppointmentId,
        string PatientId,
        string PatientName,
        string DoctorId,
        string DoctorName,
        DateTime OldReservedAt,
        DateTime NewReservedAt) : IDomainEvent;

    public record PatientContactInfoUpdatedDomainEvent(
        string PatientId,
        string Name,
        string Email,
        string PhoneNumber) : IDomainEvent;
} 