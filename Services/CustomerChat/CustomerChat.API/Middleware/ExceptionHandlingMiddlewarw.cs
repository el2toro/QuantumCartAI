using CustomerChat.Domain.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace CustomerChat.API.Middleware;

/// <summary>
/// Catches all unhandled exceptions and translates them to consistent JSON error responses.
/// Keeps controllers clean — they never need try/catch.
/// </summary>
public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title, errors) = exception switch
        {
            ValidationException ve => (
                HttpStatusCode.BadRequest,
                "Validation failed",
                ve.Errors.Select(e => e.ErrorMessage).ToArray()),

            DomainException de => (
                HttpStatusCode.BadRequest,
                "Business rule violation",
                new[] { de.Message }),

            NotFoundException nfe => (
                HttpStatusCode.NotFound,
                "Resource not found",
                new[] { nfe.Message }),

            UnauthorizedAccessException => (
                HttpStatusCode.Unauthorized,
                "Unauthorized",
                new[] { "You are not authorized to perform this action." }),

            _ => (
                HttpStatusCode.InternalServerError,
                "An unexpected error occurred",
                new[] { "Please try again later." })
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            title,
            status = (int)statusCode,
            errors,
            traceId = context.TraceIdentifier
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
    }
}
