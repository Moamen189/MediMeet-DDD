using DoctorAvailability.Api.DoctorAvailabilityShared;
using DoctorAvailability.Business.Services;
using DoctorAvailability.DataAccess.IRepository;
using DoctorAvailability.DataAccess.Repository;
using DoctorAvailability.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAvailability.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDAModule(this IServiceCollection services)
    {
        services.AddScoped<ISlotRepository, SlotRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IDoctorAvailabilityApi, DoctorAvailabilityApi>();
        services.AddScoped<SlotServices>();
        return services;
    }

}