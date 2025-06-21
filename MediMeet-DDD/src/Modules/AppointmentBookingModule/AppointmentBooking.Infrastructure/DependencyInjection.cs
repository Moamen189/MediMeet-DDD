using AppointmentBooking.Domain.IRepository;
using AppointmentBooking.Infrastructure.Extensions;
using AppointmentBooking.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddABMInfra(this IServiceCollection services)
    {
        // Use the extension method from Extensions class to register repositories
        services.AddAppointmentBookingInfrastructure();
        
        // Register MediatR handlers
        services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining<IAppointmentRepository>()); 

        return services;
    }
}