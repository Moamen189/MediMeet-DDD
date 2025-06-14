using Shared.RootEntity;

namespace AppointmentBooking.Domain.DomainEvents
{
	public class AppointmentCreatedDomainEvent(
		string AppointmentId,
		string PatientId,
		string PatientName,
		string DoctorId,
		string DoctorName,
		DateTime ReservedAt) : IDomainEvent;

	public class AppointmentCancelledDomainEvent(
		string AppointmentId,
		string PatientId,
		string PatientName,
		string DoctorId,
		string DoctorName,
		DateTime ReservedAt,
		string CancellationReason) : IDomainEvent
	{
	}

	public class AppointmentConfirmedDomainEvent(
		string AppointmentId,
		string PatientId,
		string PatientName,
		string DoctorId,
		string DoctorName,
		DateTime ReservedAt) : IDomainEvent
	{
	}

	public class AppointmentRescheduledDomainEvent(
		string AppointmentId,
		string PatientId,
		string PatientName,
		string DoctorId,
		string DoctorName,
		DateTime OldReservedAt,
		DateTime NewReservedAt) : IDomainEvent
	{
	}

	public record PatientContactInfoUpdatedDomainEvent(
		string PatientId,
		string Name,
		string Email,
		string PhoneNumber) : IDomainEvent;
}