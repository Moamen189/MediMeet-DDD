using AppointmentBooking.Domain.DomainModels;
using Shared.EventBus;

namespace AppointmentBooking.Domain.DomainEvents;

public record PatientContactInfoUpdatedDomainEvent : IDomainEvent
{
    public PatientContactInfoUpdatedDomainEvent(
        PatientId patientId,
        EmailAddress newEmail,
        PhoneNumber newPhone)
    {
        PatientId = patientId;
        NewEmail = newEmail;
        NewPhone = newPhone;
    }

    public PatientId PatientId { get; }
    public EmailAddress NewEmail { get; }
    public PhoneNumber NewPhone { get; }
} 