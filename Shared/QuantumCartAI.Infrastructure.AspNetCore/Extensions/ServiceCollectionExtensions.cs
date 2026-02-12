using Microsoft.Extensions.DependencyInjection;
using QuantumCartAI.Shared.Infrastructure.AspNetCore.Session;

namespace QuantumCartAI.Shared.Infrastructure.AspNetCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddECommerceAspNetInfrastructure(this IServiceCollection services)
    {
        // Already required for CurrentSession to work
        services.AddHttpContextAccessor();

        // Register our helper as scoped (matches HttpContext lifetime)
        services.AddScoped<CurrentSession>();

        return services;
    }
}
