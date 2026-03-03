using BuildingBlocks.Behaviors;
using CustomerChat.Application.Features.Agents.Commands;
using CustomerChat.Application.Features.Conversations.Commands;
using CustomerChat.Application.Features.Conversations.Queries;
using CustomerChat.Application.Features.Messages.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerChat.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(ApplicationServiceRegistration).Assembly,
            typeof(AssignAgentCommandHandler).Assembly,
            typeof(RequestHandoffCommandHandler).Assembly,
            typeof(ResolveConversationCommandHandler).Assembly,
            typeof(StartConversationCommandHandler).Assembly,

            typeof(GetConversationByIdQueryHandler).Assembly,
            typeof(SendCustomerMessageCommandHandler).Assembly,
            typeof(SendAgentMessageCommandHandler).Assembly
        };

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }
}
