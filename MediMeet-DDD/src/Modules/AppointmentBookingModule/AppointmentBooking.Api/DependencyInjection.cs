using AppointmentBooking.Application.AppointmentBookingShared;
using AppointmentBooking.Application.CreateAppointment;
using AppointmentBooking.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddABModule(this IServiceCollection services)
	{
		services.AddScoped<CreateAppointmentHandler>();
		services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining<CreateAppointmentHandler>());
		services.AddScoped<IAppointmentBookingApi, AppointmentBookingApi>();

		return services;
	}
}