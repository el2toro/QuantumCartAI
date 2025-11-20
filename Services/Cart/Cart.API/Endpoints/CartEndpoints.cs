using Cart.Application.Handlers.Commands;
using Cart.Application.Handlers.Queries;
using Cart.Domain.ValueObjects;

namespace Cart.API.Endpoints;

public class CartEndpoints : ICarterModule
{
    public record AddItemRequest(Guid? CustomerId, Guid? CartId, Guid ProductId, int Quantity, Currency Currency);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("cart/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var cart = await sender.Send(new GetCartQuery(customerId));
            return Results.Ok(cart);
        });

        app.MapPost("cart", async (AddItemRequest request, ISender sender) =>
        {
            var command = request.Adapt<AddItemCommand>();
            var response = await sender.Send(command);

            return Results.Ok(response.Cart);
        })
        .WithDisplayName("Cart")
        .WithDescription("Cart")
        .WithSummary("Add Item To Cart")
        .Produces(StatusCodes.Status200OK);

        app.MapGet("cart/{cartId}/items", (Guid cartId, ISender sender) =>
        {
            // Implementation to get cart by customerId
            return Results.Ok();
        });

        app.MapPost("cart/{cartId}/checkout", (Guid cartId, ISender sender) =>
        {
            // Implementation to get cart by customerId
            return Results.Ok();
        });

        app.MapPut("cart/{cartId}/items/{productId}/quantity", (int quantity, ISender sender) =>
        {
            // Implementation to get cart by customerId
            return Results.Ok();
        });

        app.MapDelete("cart/{cartId}/items/{productId}", (Guid productId, ISender sender) =>
        {
            // Implementation to get cart by customerId
            return Results.Ok();
        });

        app.MapDelete("cart/{cartId}", (Guid cartId, ISender sender) =>
        {
            // Implementation to get cart by customerId
            return Results.Ok();
        });
    }
}
