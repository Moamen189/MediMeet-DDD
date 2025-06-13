using DoctorAppointmentManagement.AdapterInfra.Repository;
using DoctorAppointmentManagement.Core.OutputPorts.IRepository;
using DoctorAppointmentManagement.Shared.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAppointmentManagement.AdapterInfra;

public static class DependencyInjection
{
    public static IServiceCollection AddDAMInfra(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining<AppointmentConfirmedEvent>());

        return services;
    }
}