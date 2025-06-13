using Shared.RootEntity;

namespace DoctorAvailability.Business.DomainModels;

public record DoctorId
{
    public int Value { get; }

    private DoctorId(int value)
    {
        if (value <= 0)
        {
            throw new DomainException("Doctor ID must be positive");
        }
        Value = value;
    }

    public static DoctorId From(int id) => new(id);
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

    public static DoctorName From(string name) => new(name);
}

public record Specialty
{
    public string Value { get; }

    private Specialty(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Specialty cannot be empty");
        }
        if (value.Length > 50)
        {
            throw new DomainException("Specialty cannot be longer than 50 characters");
        }
        Value = value;
    }

    public static Specialty From(string specialty) => new(specialty);
}

public record Money
{
    public decimal Value { get; }

    private Money(decimal value)
    {
        if (value < 0)
        {
            throw new DomainException("Money amount cannot be negative");
        }
        Value = value;
    }

    public static Money From(decimal amount) => new(amount);
}

public record TimeRange
{
    public DateTime Start { get; }
    public DateTime End { get; }
    public TimeSpan Duration => End - Start;

    private TimeRange(DateTime start, DateTime end)
    {
        if (start >= end)
        {
            throw new DomainException("End time must be after start time");
        }
        Start = start.ToUniversalTime();
        End = end.ToUniversalTime();
    }

    public static TimeRange Create(DateTime start, DateTime end) => new(start, end);

    public bool OverlapsWith(TimeRange other)
    {
        return Start < other.End && other.Start < End;
    }
} 