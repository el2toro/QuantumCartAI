using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

/// <summary>
/// MediatR pipeline behavior that validates all commands/queries before they reach handlers.
/// This keeps validation logic out of handlers and centralizes it.
/// </summary>
/// 

// TODO: Use CQRS Command and query interfaces
public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);
        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count != 0)
        {
            logger.LogWarning("Validation failed for {RequestName}: {Errors}",
                typeof(TRequest).Name,
                string.Join(", ", failures.Select(f => f.ErrorMessage)));

            throw new ValidationException(failures);
        }

        return await next();
    }
}
