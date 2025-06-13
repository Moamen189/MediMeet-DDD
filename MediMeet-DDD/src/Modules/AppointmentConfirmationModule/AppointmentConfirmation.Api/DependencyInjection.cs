using AppointmentConfirmation.Api.Controllers;
using AppointmentConfirmation.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentConfirmation.Api
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddACModule(this IServiceCollection services)
		{
			services.AddScoped<INotificationApi, NotificationApi>();
			return services;
		}
	}
}
