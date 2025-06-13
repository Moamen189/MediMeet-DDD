using DoctorAppointmentManagement.Core.Domain.Enums;

namespace DoctorAppointmentManagement.Core.Application.Dtos;

public class AppointmentConfirmationDto
{
    public Guid Id { get; set; }
    public Guid AppointmentId { get; set; }
    public Guid SlotId { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; }
    public AppointmentStatus Status { get; set; }
    public string? Comments { get; set; }
    public DateTime ReservedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateAppointmentConfirmationDto
{
    public Guid AppointmentId { get; set; }
    public Guid SlotId { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; }
    public DateTime ReservedAt { get; set; }
    public string? Comments { get; set; }
}

public class UpdateAppointmentStatusDto
{
    public Guid AppointmentId { get; set; }
    public string? Comments { get; set; }
} 