using Shared.RootEntity;

namespace AppointmentBooking.Domain.DomainModels;

public record AppointmentId
{
    public Guid Value { get; }

    private AppointmentId(Guid value)
    {
        Value = value;
    }

    public static AppointmentId Create()
    {
        return new AppointmentId(Guid.NewGuid());
    }

    public static AppointmentId From(Guid id)
    {
        return new AppointmentId(id);
    }
}

public record PatientId
{
    public int Value { get; }

    private PatientId(int value)
    {
        if (value <= 0)
        {
            throw new DomainException("Patient ID must be positive");
        }
        Value = value;
    }

    public static PatientId From(int id)
    {
        return new PatientId(id);
    }
}

public record PatientName
{
    public string Value { get; }

    private PatientName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Patient name cannot be empty");
        }
        if (value.Length > 100)
        {
            throw new DomainException("Patient name cannot be longer than 100 characters");
        }
        Value = value;
    }

    public static PatientName From(string name)
    {
        return new PatientName(name);
    }
}

public record DoctorName
{
    public string Value { get; }

    private DoctorName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Doctor name cannot be empty");
        }
        if (value.Length > 100)
        {
            throw new DomainException("Doctor name cannot be longer than 100 characters");
        }
        Value = value;
    }

    public static DoctorName From(string name)
    {
        return new DoctorName(name);
    }
}

public record AppointmentDateTime
{
    public DateTime Value { get; }

    private AppointmentDateTime(DateTime value)
    {
        Value = value.ToUniversalTime();
    }

    public static AppointmentDateTime From(DateTime dateTime)
    {
        return new AppointmentDateTime(dateTime);
    }
} 