using DoctorAppointmentManagement.Core.Domain.Events;
using Shared.RootEntity;

namespace DoctorAppointmentManagement.Core.Domain.Models;

public class AppointmentConfirmation : AggregateRoot
{
	private AppointmentConfirmation() { } // For EF Core

	private AppointmentConfirmation(
		AppointmentConfirmationId id,
		AppointmentId appointmentId,
		SlotId slotId,
		PatientInfo patientInfo,
		AppointmentDateTime reservedAt,
		AppointmentStatus status,
		string? comments = null)
	{
		Id = id;
		AppointmentId = appointmentId;
		SlotId = slotId;
		PatientInfo = patientInfo;
		ReservedAt = reservedAt;
		Status = status;
		Comments = comments;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new AppointmentStatusChangedDomainEvent(this));
	}

	public AppointmentConfirmationId Id { get; private set; }
	public AppointmentId AppointmentId { get; private set; }
	public SlotId SlotId { get; private set; }
	public PatientInfo PatientInfo { get; private set; }
	public AppointmentDateTime ReservedAt { get; private set; }
	public AppointmentStatus Status { get; private set; }
	public string? Comments { get; private set; }
	public DateTime UpdatedAt { get; private set; }

	public static AppointmentConfirmation Create(
		Guid appointmentId,
		Guid slotId,
		int patientId,
		string patientName,
		DateTime reservedAt,
		string? comments = null)
	{
		return new AppointmentConfirmation(
			AppointmentConfirmationId.Create(),
			AppointmentId.From(appointmentId),
			SlotId.From(slotId),
			PatientInfo.Create(patientId.ToString(), patientName, "", ""),
			AppointmentDateTime.From(reservedAt),
			AppointmentStatus.Pending,
			comments
		);
	}

	public void Confirm(string? comments = null)
	{
		if (Status != AppointmentStatus.Pending)
		{
			throw new DomainException("Can only confirm pending appointments");
		}

		Status = AppointmentStatus.Confirmed;
		Comments = comments;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new AppointmentStatusChangedDomainEvent(this));
	}

	private void AddDomainEvent(AppointmentStatusChangedDomainEvent appointmentStatusChangedDomainEvent)
	{
		throw new NotImplementedException();
	}

	public void Cancel(string reason)
	{
		if (Status != AppointmentStatus.Pending && Status != AppointmentStatus.Confirmed)
		{
			throw new DomainException("Can only cancel pending or confirmed appointments");
		}

		if (string.IsNullOrWhiteSpace(reason))
		{
			throw new DomainException("Cancellation reason is required");
		}

		Status = AppointmentStatus.Cancelled;
		Comments = reason;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new AppointmentStatusChangedDomainEvent(this));
	}

	public bool CanBeModified()
	{
		return Status == AppointmentStatus.Pending;
	}
}