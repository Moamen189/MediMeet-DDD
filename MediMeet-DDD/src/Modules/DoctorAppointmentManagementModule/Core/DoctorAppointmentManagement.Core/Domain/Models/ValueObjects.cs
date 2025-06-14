using Shared.RootEntity;

namespace DoctorAppointmentManagement.Core.Domain.Models;

public record AppointmentConfirmationId
{
	public Guid Value { get; }
	private AppointmentConfirmationId(Guid value) => Value = value;
	public static AppointmentConfirmationId Create() => new(Guid.NewGuid());
	public static AppointmentConfirmationId From(Guid id) => new(id);
}

public record AppointmentId
{
	public Guid Value { get; }
	private AppointmentId(Guid value) => Value = value;
	public static AppointmentId From(Guid id) => new(id);
}

public record SlotId
{
	public Guid Value { get; }
	private SlotId(Guid value) => Value = value;
	public static SlotId From(Guid id) => new(id);
}

public class PatientInfo
{
	public string Id { get; private set; }
	public string Name { get; private set; }
	public string Email { get; private set; }
	public string PhoneNumber { get; private set; }

	private PatientInfo() { }

	public static PatientInfo Create(string id, string name, string email, string phoneNumber)
	{
		if (string.IsNullOrWhiteSpace(id))
			throw new DomainException("Patient ID cannot be empty");

		if (string.IsNullOrWhiteSpace(name))
			throw new DomainException("Patient name cannot be empty");

		if (string.IsNullOrWhiteSpace(email))
			throw new DomainException("Patient email cannot be empty");

		return new PatientInfo
		{
			Id = id,
			Name = name,
			Email = email,
			PhoneNumber = phoneNumber
		};
	}

	private static bool IsValidEmail(string email)
	{
		try
		{
			var addr = new System.Net.Mail.MailAddress(email);
			return addr.Address == email;
		}
		catch
		{
			return false;
		}
	}
}

public class AppointmentDateTime
{
	public DateTime StartTime { get; private set; }
	public DateTime EndTime { get; private set; }

	private AppointmentDateTime() { }

	public static AppointmentDateTime Create(DateTime startTime, DateTime endTime)
	{
		if (startTime >= endTime)
			throw new DomainException("Start time must be before end time");

		if (startTime < DateTime.UtcNow)
			throw new DomainException("Cannot schedule appointments in the past");

		return new AppointmentDateTime
		{
			StartTime = startTime,
			EndTime = endTime
		};
	}

	public bool Overlaps(AppointmentDateTime other)
	{
		return StartTime < other.EndTime && EndTime > other.StartTime;
	}
}

public enum AppointmentStatus
{
	Pending,
	Confirmed,
	Cancelled,
	Completed,
	NoShow
}