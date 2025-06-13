namespace AppointmentBooking.Shared.Dtos
{
	public class AppointmentDto
	{
		public Guid Id { get; set; }
		public Guid SlotId { get; set; }
		public int PatientId { get; set; }
		public string PatientName { get; set; }
		public DateTime ReservedAt { get; set; }
	}
}
