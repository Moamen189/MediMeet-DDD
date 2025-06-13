namespace DoctorAppointmentManagement.Core.Dtos;

public class AppointmentConfirmationDto
{
    public Guid AppointmentId { get; set; }
    public string? Comments { get; set; }
    public Guid SlotId { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; }
    public DateTime ReservedAt { get; set; }
    
}