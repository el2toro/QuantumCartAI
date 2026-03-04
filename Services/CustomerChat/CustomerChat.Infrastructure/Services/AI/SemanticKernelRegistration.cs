using CustomerChat.Application.Common.Interfaces;
using CustomerChat.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;

namespace CustomerChat.Infrastructure.Services.AI;

public static class SemanticKernelRegistration
{
    /// <summary>
    /// Registers Semantic Kernel and all AI services.
    /// Called from InfrastructureServiceRegistration.
    /// </summary>
    public static IServiceCollection AddSemanticKernel(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Bind and validate options at startup — fail fast if config is missing
        services
            .AddOptions<AzureOpenAIOptions>()
            .Bind(configuration.GetSection(AzureOpenAIOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // Conversation memory store — singleton so history persists across requests
        // In production: replace with a Redis-backed implementation
        services.AddSingleton<ConversationHistoryStore>();

        // Build the Semantic Kernel — registered as scoped so plugins can
        // use scoped services like IUnitOfWork
        services.AddScoped<Kernel>(sp =>
        {
            var opts = sp.GetRequiredService<IOptions<AzureOpenAIOptions>>().Value;

            var builder = Kernel.CreateBuilder()
                .AddAzureOpenAIChatCompletion(
                    deploymentName: opts.DeploymentName,
                    endpoint: opts.Endpoint,
                    apiKey: opts.ApiKey,
                    modelId: opts.ModelId);

            var kernel = builder.Build();

            // Register the ecommerce plugin so the AI can call these tools
            var unitOfWork = sp.GetRequiredService<IUnitOfWork>();
            kernel.Plugins.AddFromObject(new EcommercePlugin(unitOfWork), "Ecommerce");

            return kernel;
        });

        // Register the bot service — UseStub flag allows local dev without Azure
        services.AddScoped<IBotResponseService>(sp =>
        {
            var opts = sp.GetRequiredService<IOptions<AzureOpenAIOptions>>().Value;

            if (opts.UseStub)
            {
                var logger = sp.GetRequiredService<ILogger<StubBotResponseService>>();
                return new StubBotResponseService(logger);
            }

            return new SemanticKernelBotService(
                sp.GetRequiredService<Kernel>(),
                sp.GetRequiredService<ConversationHistoryStore>(),
                sp.GetRequiredService<IUnitOfWork>(),
                sp.GetRequiredService<ILogger<SemanticKernelBotService>>(),
                sp.GetRequiredService<IOptions<AzureOpenAIOptions>>());
        });

        return services;
    }
}
