using AppointmentBooking.Domain.DomainEvents;
using Shared.RootEntity;

namespace AppointmentBooking.Domain.DomainModels;

public class Appointment : AggregateRoot
{
	private const int MAX_RESCHEDULE_ATTEMPTS = 3;
	private int _rescheduleCount;

	protected Appointment() { } // For EF Core

	private Appointment(
		Guid slotId,
		PatientId patientId,
		PatientName patientName,
		DoctorName doctorName,
		AppointmentDateTime reservedAt)
	{
		Id = AppointmentId.Create();
		SlotId = slotId;
		PatientId = patientId;
		PatientName = patientName;
		DoctorName = doctorName;
		ReservedAt = reservedAt;
		Status = AppointmentStatus.Pending;
		_rescheduleCount = 0;

		AddDomainEvent(new AppointmentCreatedDomainEvent(this));
	}

	public AppointmentId Id { get; private set; }
	public Guid SlotId { get; private set; }
	public PatientId PatientId { get; private set; }
	public PatientName PatientName { get; private set; }
	public DoctorName DoctorName { get; private set; }
	public AppointmentStatus Status { get; private set; }
	public AppointmentDateTime ReservedAt { get; private set; }

	public static Appointment Schedule(
		Guid slotId,
		PatientId patientId,
		PatientName patientName,
		DoctorName doctorName,
		AppointmentDateTime reservedAt)
	{
		if (reservedAt.Value < DateTime.UtcNow)
		{
			throw new DomainException("Cannot schedule appointment in the past");
		}

		// Add minimum notice period rule
		var minimumNotice = DateTime.UtcNow.AddHours(1);
		if (reservedAt.Value < minimumNotice)
		{
			throw new DomainException("Appointments must be scheduled at least 1 hour in advance");
		}

		return new Appointment(slotId, patientId, patientName, doctorName, reservedAt);
	}

	public void Cancel()
	{
		EnsureCanBeCancelled();

		Status = AppointmentStatus.Cancelled;
		AddDomainEvent(new AppointmentCancelledDomainEvent(this));
	}

	public void Confirm()
	{
		if (Status != AppointmentStatus.Pending)
		{
			throw new DomainException("Can only confirm pending appointments");
		}

		Status = AppointmentStatus.Confirmed;
		AddDomainEvent(new AppointmentConfirmedDomainEvent(this));
	}

	public void Reschedule(AppointmentDateTime newDateTime)
	{
		EnsureCanBeRescheduled();

		if (_rescheduleCount >= MAX_RESCHEDULE_ATTEMPTS)
		{
			throw new DomainException($"Cannot reschedule more than {MAX_RESCHEDULE_ATTEMPTS} times");
		}

		if (newDateTime.Value <= DateTime.UtcNow)
		{
			throw new DomainException("Cannot reschedule to a past date/time");
		}

		var minimumNotice = DateTime.UtcNow.AddHours(1);
		if (newDateTime.Value < minimumNotice)
		{
			throw new DomainException("Appointments must be rescheduled at least 1 hour in advance");
		}

		var oldDateTime = ReservedAt;
		ReservedAt = newDateTime;
		_rescheduleCount++;

		AddDomainEvent(new AppointmentRescheduledDomainEvent(this, oldDateTime));
	}

	private void EnsureCanBeCancelled()
	{
		if (Status != AppointmentStatus.Pending && Status != AppointmentStatus.Confirmed)
		{
			throw new DomainException("Only pending or confirmed appointments can be cancelled");
		}

		var cancellationDeadline = ReservedAt.Value.AddHours(-2);
		if (DateTime.UtcNow > cancellationDeadline)
		{
			throw new DomainException("Appointments can only be cancelled at least 2 hours before the scheduled time");
		}
	}

	private void EnsureCanBeRescheduled()
	{
		if (Status != AppointmentStatus.Pending && Status != AppointmentStatus.Confirmed)
		{
			throw new DomainException("Only pending or confirmed appointments can be rescheduled");
		}

		var rescheduleDeadline = ReservedAt.Value.AddHours(-2);
		if (DateTime.UtcNow > rescheduleDeadline)
		{
			throw new DomainException("Appointments can only be rescheduled at least 2 hours before the scheduled time");
		}
	}
}

public enum AppointmentStatus
{
	Pending,
	Confirmed,
	Cancelled
}