using AppointmentBooking.Domain.IRepository;
using AppointmentBooking.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddABMInfra(this IServiceCollection services)
    {
       
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddMediatR(m=> m.RegisterServicesFromAssemblyContaining<IAppointmentRepository>()); 


        return services;
    }
}