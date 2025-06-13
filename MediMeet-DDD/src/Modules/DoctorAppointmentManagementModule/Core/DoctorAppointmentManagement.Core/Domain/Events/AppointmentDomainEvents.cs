using DoctorAppointmentManagement.Core.Domain.Models;
using Shared.EventBus;

namespace DoctorAppointmentManagement.Core.Domain.Events;

public record AppointmentStatusChangedDomainEvent : IDomainEvent
{
    public AppointmentStatusChangedDomainEvent(AppointmentConfirmation appointment)
    {
        AppointmentId = appointment.AppointmentId.Value;
        SlotId = appointment.SlotId.Value;
        PatientId = appointment.PatientInfo.Id;
        PatientName = appointment.PatientInfo.Name;
        Status = appointment.Status;
        Comments = appointment.Comments;
        UpdatedAt = appointment.UpdatedAt;
    }

    public Guid AppointmentId { get; }
    public Guid SlotId { get; }
    public int PatientId { get; }
    public string PatientName { get; }
    public AppointmentStatus Status { get; }
    public string? Comments { get; }
    public DateTime UpdatedAt { get; }
} 