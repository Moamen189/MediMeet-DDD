using Shared.RootEntity;

namespace AppointmentBooking.Domain.DomainModels;

public class Patient : AggregateRoot
{
    private Patient() { } // For EF Core

    private Patient(
        PatientId id,
        PatientName name,
        EmailAddress email,
        PhoneNumber phoneNumber)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public PatientId Id { get; private set; }
    public PatientName Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }

    public static Patient Create(
        int id,
        string name,
        string email,
        string phoneNumber)
    {
        return new Patient(
            PatientId.From(id),
            PatientName.From(name),
            EmailAddress.From(email),
            PhoneNumber.From(phoneNumber)
        );
    }

    public void UpdateContactInfo(EmailAddress newEmail, PhoneNumber newPhone)
    {
        Email = newEmail;
        PhoneNumber = newPhone;
        AddDomainEvent(new PatientContactInfoUpdatedDomainEvent(Id, Email, PhoneNumber));
    }
}

public record EmailAddress
{
    public string Value { get; }

    private EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Email address cannot be empty");
        
        if (!value.Contains("@"))
            throw new DomainException("Invalid email address format");
        
        Value = value;
    }

    public static EmailAddress From(string email) => new(email);
}

public record PhoneNumber
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Phone number cannot be empty");
        
        // Add more validation as needed
        Value = value;
    }

    public static PhoneNumber From(string phoneNumber) => new(phoneNumber);
}