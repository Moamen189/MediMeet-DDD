using AppointmentBooking.Domain.DomainModels;
using AppointmentBooking.Infrastructure.Entities;

namespace AppointmentBooking.Infrastructure.Extentions;

public static class Extentionss
{
	public static AppointmentEntity ToEntity(this Appointment appointment)
	{
		return new AppointmentEntity
		{
			Id = appointment.Id,
			SlotId = appointment.SlotId,
			PatientId = appointment.PatientId,
			PatientName = appointment.PatientName,
			ReservedAt = appointment.ReservedAt
		};
	}

	public static Appointment ToModel(this AppointmentEntity appointment)
	{
		return new Appointment
		{
			Id = appointment.Id,
			SlotId = appointment.SlotId,
			PatientId = appointment.PatientId,
			PatientName = appointment.PatientName,
			ReservedAt = appointment.ReservedAt
		};
	}
	public static PatientEntity ToEntity(this Patient patient)
	{
		return new PatientEntity
		{
			Id = patient.Id,
			Name = patient.Name,
			Email = patient.Email,
			PhoneNumber = patient.PhoneNumber
		};
	}
}