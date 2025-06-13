using AppointmentBooking.Shared.Dtos;

namespace AppointmentBooking.Shared.Contracts
{
	public interface IAppointmentBookingApi
	{
		Task<List<AppointmentDto>> GetUpComingAppointments();
	}
}
