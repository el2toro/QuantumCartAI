using CustomerChat.Application.Common.Interfaces;
using CustomerChat.Domain.Repositories;
using CustomerChat.Infrastructure.Hubs;
using CustomerChat.Infrastructure.Persistence;
using CustomerChat.Infrastructure.Persistence.Repositories;
using CustomerChat.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CustomerChat.Infrastructure.Services.AI;
using Microsoft.EntityFrameworkCore;

namespace CustomerChat.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ── Database ──────────────────────────────────────────────────────────
        services.AddDbContext<ChatDbContext>(opts =>
            opts.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sql => sql.EnableRetryOnFailure(3)));

        // ── Repositories ──────────────────────────────────────────────────────
        services.AddScoped<IConversationRepository, ConversationRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IAgentRepository, AgentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // ── Application Services ──────────────────────────────────────────────
        services.AddScoped<INotificationService, SignalRNotificationService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddHttpContextAccessor();

        // ── Semantic Kernel + Azure OpenAI ────────────────────────────────────
        // IBotResponseService is registered inside AddSemanticKernel.
        // Set AzureOpenAI:UseStub=true in appsettings to use the keyword stub locally.
        services.AddSemanticKernel(configuration);

        // ── SignalR ───────────────────────────────────────────────────────────
        services.AddSignalR(opts =>
        {
            opts.EnableDetailedErrors = true; // Disable in production
        });

        // ── Authentication ────────────────────────────────────────────────────
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
                };

                // Allow JWT in SignalR query string (browser limitation)
                opts.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments(ChatHub.HubUrl))
                            context.Token = accessToken;
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}
