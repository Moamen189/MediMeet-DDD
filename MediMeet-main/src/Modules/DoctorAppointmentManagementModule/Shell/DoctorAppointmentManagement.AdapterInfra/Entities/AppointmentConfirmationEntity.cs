namespace DoctorAppointmentManagement.AdapterInfra.Entities;

public class AppointmentConfirmationEntity
{
    public Guid Id { get; set; }
    public Guid AppointmentId { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Comments { get; set; }
    public Guid SlotId { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; }
    public int AppointmentStatus { get; set; }
    public DateTime ReservedAt { get; set; }
}