using Carter;
using Mapster;
using MediatR;

namespace Payment.API.Payments.ConfirmPayment;

public class ConfirmPaymentEndpoint : ICarterModule
{
    public record ConfirmPaymentRequest(Guid OrderId, Guid CustomerId, decimal Amount, string PaymentMethod);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/payment/confirm", async (ConfirmPaymentRequest request, ISender sender) =>
        {
            var command = request.Adapt<ConfirmPaymentCommand>();
            await sender.Send(command);

            return Results.Ok();
        });
    }
}
