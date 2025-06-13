using DoctorAppointmentManagement.Core.InPutPorts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAppointmentManagement.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddDAMModule(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentServices, AppointmentServices>();
        return services;
    }
}