using Shared.RootEntity;

namespace AppointmentConfirmation.Api.Domain.Models;

public record NotificationId
{
	public Guid Value { get; }

	private NotificationId(Guid value)
	{
		Value = value;
	}

	public static NotificationId Create() => new(Guid.NewGuid());
	public static NotificationId From(Guid id) => new(id);
}

public class NotificationContent
{
	public string Subject { get; private set; }
	public string Body { get; private set; }

	private NotificationContent() { }

	public static NotificationContent Create(string subject, string body)
	{
		if (string.IsNullOrWhiteSpace(subject))
			throw new DomainException("Notification subject cannot be empty");

		if (string.IsNullOrWhiteSpace(body))
			throw new DomainException("Notification body cannot be empty");

		return new NotificationContent
		{
			Subject = subject,
			Body = body
		};
	}

	public static NotificationContent CreateAppointmentConfirmation(
		string patientName,
		DateTime appointmentDateTime,
		string doctorName)
	{
		var subject = "Appointment Confirmation";
		var body = $"""
            Dear {patientName},

            Your appointment with {doctorName} has been confirmed for {appointmentDateTime:f}.

            Please arrive 10 minutes before your scheduled time.

            Best regards,
            MediMeet Team
            """;

		return new NotificationContent
		{
			Subject = subject,
			Body = body
		};
	}

	public static NotificationContent CreateAppointmentCancellation(
		string patientName,
		DateTime appointmentDateTime,
		string doctorName,
		string reason)
	{
		var subject = "Appointment Cancellation";
		var body = $"""
            Dear {patientName},

            Your appointment with {doctorName} scheduled for {appointmentDateTime:f} has been cancelled.

            Reason: {reason}

            Please reschedule at your earliest convenience.

            Best regards,
            MediMeet Team
            """;

		return new NotificationContent
		{
			Subject = subject,
			Body = body
		};
	}

	public static NotificationContent CreateAppointmentReminder(
		string patientName,
		DateTime appointmentDateTime,
		string doctorName)
	{
		var subject = "Appointment Reminder";
		var body = $"""
            Dear {patientName},

            This is a reminder for your upcoming appointment with {doctorName} scheduled for {appointmentDateTime:f}.

            Please arrive 10 minutes before your scheduled time.

            Best regards,
            MediMeet Team
            """;

		return new NotificationContent
		{
			Subject = subject,
			Body = body
		};
	}
}

public class NotificationRecipient
{
	public string Email { get; private set; }
	public string Name { get; private set; }

	private NotificationRecipient() { }

	public static NotificationRecipient Create(string email, string name)
	{
		if (string.IsNullOrWhiteSpace(email))
			throw new DomainException("Recipient email cannot be empty");

		if (!IsValidEmail(email))
			throw new DomainException("Invalid email format");

		if (string.IsNullOrWhiteSpace(name))
			throw new DomainException("Recipient name cannot be empty");

		return new NotificationRecipient
		{
			Email = email,
			Name = name
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

public enum NotificationType
{
	AppointmentConfirmation,
	AppointmentCancellation,
	AppointmentReminder
}

public enum NotificationStatus
{
	Pending,
	Sent,
	Failed
}