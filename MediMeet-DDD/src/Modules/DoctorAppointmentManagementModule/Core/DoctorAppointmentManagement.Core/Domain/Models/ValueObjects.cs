namespace DoctorAppointmentManagement.Core.Domain.Models;

public record AppointmentConfirmationId
{
    public Guid Value { get; }
    private AppointmentConfirmationId(Guid value) => Value = value;
    public static AppointmentConfirmationId Create() => new(Guid.NewGuid());
    public static AppointmentConfirmationId From(Guid id) => new(id);
}

public record AppointmentId
{
    public Guid Value { get; }
    private AppointmentId(Guid value) => Value = value;
    public static AppointmentId From(Guid id) => new(id);
}

public record SlotId
{
    public Guid Value { get; }
    private SlotId(Guid value) => Value = value;
    public static SlotId From(Guid id) => new(id);
} 