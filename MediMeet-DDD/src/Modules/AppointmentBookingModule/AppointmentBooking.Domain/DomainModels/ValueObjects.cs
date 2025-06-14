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
        if (value < DateTime.UtcNow)
        {
            throw new DomainException("Cannot set appointment date/time in the past");
        }

        // Ensure minimum notice period
        var minimumNotice = DateTime.UtcNow.AddHours(1);
        if (value < minimumNotice)
        {
            throw new DomainException("Appointments must be scheduled at least 1 hour in advance");
        }

        Value = value.ToUniversalTime();
    }

    public static AppointmentDateTime Create(DateTime dateTime)
    {
        return new AppointmentDateTime(dateTime);
    }

    public static AppointmentDateTime From(DateTime dateTime)
    {
        return new AppointmentDateTime(dateTime);
    }

    public bool IsInPast()
    {
        return Value < DateTime.UtcNow;
    }

    public bool IsWithinCancellationPeriod()
    {
        var cancellationDeadline = Value.AddHours(-2);
        return DateTime.UtcNow > cancellationDeadline;
    }

    public bool IsWithinReschedulePeriod()
    {
        var rescheduleDeadline = Value.AddHours(-2);
        return DateTime.UtcNow > rescheduleDeadline;
    }
} 