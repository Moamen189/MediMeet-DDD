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

public record NotificationContent
{
	public string Subject { get; }
	public string Body { get; }

	private NotificationContent(string subject, string body)
	{
		if (string.IsNullOrWhiteSpace(subject))
			throw new CannotUnloadAppDomainException("Notification subject cannot be empty");

		if (string.IsNullOrWhiteSpace(body))
			throw new CannotUnloadAppDomainException("Notification body cannot be empty");

		Subject = subject;
		Body = body;
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

		return new NotificationContent(subject, body);
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

		return new NotificationContent(subject, body);
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

		return new NotificationContent(subject, body);
	}
}

public record NotificationRecipient
{
	public string Email { get; }
	public string Name { get; }

	private NotificationRecipient(string email, string name)
	{
		if (string.IsNullOrWhiteSpace(email))
			throw new CannotUnloadAppDomainException("Email cannot be empty");

		if (!email.Contains("@"))
			throw new CannotUnloadAppDomainException("Invalid email format");

		if (string.IsNullOrWhiteSpace(name))
			throw new CannotUnloadAppDomainException("Recipient name cannot be empty");

		Email = email;
		Name = name;
	}

	public static NotificationRecipient Create(string email, string name) => new(email, name);
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