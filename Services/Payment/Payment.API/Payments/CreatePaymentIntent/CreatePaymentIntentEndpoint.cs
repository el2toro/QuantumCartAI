using Carter;
using MediatR;

namespace Payment.API.Payments.CreatePaymentIntent;

public class CreatePaymentIntentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("confirm", (ISender sender) =>
        {

        });
    }
}
