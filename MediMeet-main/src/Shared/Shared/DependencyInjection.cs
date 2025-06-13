using Microsoft.Extensions.DependencyInjection;
using Shared.EventBus;

namespace Shared;



public static class DependencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddMediatR(m=> m.RegisterServicesFromAssemblyContaining<IDomainEvent>()); 
        return services;
    }
}