using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using QuantumCartAI.Shared.Infrastructure.AspNetCore.Middleware;
using QuantumCartAI.Shared.Infrastructure.AspNetCore.Session;

namespace QuantumCartAI.Shared.Infrastructure.AspNetCore.Extensions;

public static class AnonymousSessionMiddlewareExtensions
{
    public static IServiceCollection AddECommerceAspNetInfrastructure(this IServiceCollection services)
    {
        // Already required for CurrentSession to work
        services.AddHttpContextAccessor();

        // Register our helper as scoped (matches HttpContext lifetime)
        services.AddScoped<CurrentSession>();

        return services;
    }


    public static IApplicationBuilder UseAnonymousSession(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AnonymousSessionMiddleware>();
    }
}
