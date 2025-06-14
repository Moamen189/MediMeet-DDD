using AppointmentBooking.Domain.DomainModels;

namespace AppointmentBooking.Domain.DomainEvents;

// Renamed the class to avoid conflict with an existing definition in the same namespace
public class AppointmentRescheduledEvent
{
	public AppointmentId AppointmentId { get; }
	public Guid SlotId { get; }
	public PatientId PatientId { get; }
	public PatientName PatientName { get; }
	public DoctorName DoctorName { get; }
	public AppointmentDateTime OldDateTime { get; }
	public AppointmentDateTime NewDateTime { get; }

	public AppointmentRescheduledEvent(Appointment appointment, AppointmentDateTime oldDateTime)
	{
		AppointmentId = appointment.Id;
		SlotId = appointment.SlotId;
		PatientId = appointment.PatientId;
		PatientName = appointment.PatientName;
		DoctorName = appointment.DoctorName;
		OldDateTime = oldDateTime;
		NewDateTime = appointment.ReservedAt;
	}
}