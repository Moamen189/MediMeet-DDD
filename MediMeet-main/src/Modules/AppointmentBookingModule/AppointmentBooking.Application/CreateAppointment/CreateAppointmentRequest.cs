namespace AppointmentBooking.Application.CreateAppointment
{
    public class CreateAppointmentRequest
    {
        public Guid SlotId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime ReservedAt { get; set; }
    }
}