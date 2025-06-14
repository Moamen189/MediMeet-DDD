using AppointmentConfirmation.Api.Application.Services;
using AppointmentConfirmation.Api.Domain.Repositories;
using AppointmentConfirmation.Api.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentConfirmation.Api.Infrastructure.Extensions;

public static class Extensions
{
    public static IServiceCollection AddAppointmentConfirmationInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<NotificationService>();
        
        return services;
    }
} 