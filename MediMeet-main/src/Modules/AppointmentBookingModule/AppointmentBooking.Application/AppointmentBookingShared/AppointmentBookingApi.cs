using AppointmentBooking.Domain.IRepository;
using AppointmentBooking.Shared.Contracts;
using AppointmentBooking.Shared.Dtos;

namespace AppointmentBooking.Application.AppointmentBookingShared
{
	public class AppointmentBookingApi : IAppointmentBookingApi
	{
		private readonly IAppointmentRepository _appointmentRepository;

		public AppointmentBookingApi(IAppointmentRepository appointmentRepository)
		{
			_appointmentRepository = appointmentRepository;
		}
		public async Task<List<AppointmentDto>> GetUpComingAppointments()
		{
			var appointments = await _appointmentRepository.GetUpComingAppointments();
			var appointmentDtos = appointments.Select(a => new AppointmentDto
			{
				Id = a.Id,
				PatientId = a.PatientId,
				PatientName = a.PatientName,
				ReservedAt = a.ReservedAt,
				SlotId = a.SlotId,
				//Status = a.Status
			}).ToList();
			return appointmentDtos;

		}
	}
}
