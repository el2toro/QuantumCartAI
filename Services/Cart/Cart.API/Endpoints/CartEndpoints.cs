using Cart.Application.Handlers.Commands;
using Cart.Application.Handlers.Queries;
using Cart.Domain.ValueObjects;

namespace Cart.API.Endpoints;

public class CartEndpoints : ICarterModule
{
    public record CartItemRequest(Guid? CustomerId, Guid? CartId, Guid ProductId, int Quantity);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("cart", async (ISender sender) =>
        {
            var result = await sender.Send(new GetCartQuery());
            return Results.Ok(result.Cart);
        })
        .WithDisplayName("GetCart")
        .WithDescription("GetCart")
        .WithSummary("Get Cart By CustomerId")
        .Produces(StatusCodes.Status200OK);

        app.MapPost("cart/item", async (CartItemRequest request, ISender sender) =>
        {
            var command = request.Adapt<AddItemCommand>();

            var response = await sender.Send(command);

            return Results.Ok(response.Cart);
        })
        .WithDisplayName("AddItem")
        .WithDescription("AddItem")
        .WithSummary("Add Item To The Cart")
        .Produces(StatusCodes.Status200OK);

        app.MapPost("cart/{cartId}/checkout", async (Guid cartId, ISender sender) =>
        {
            var result = await sender.Send(new CartCheckoutCommand(cartId));
            return Results.Ok();
        });


        app.MapPut("cart/{cartId}/items/{productId}", async (CartItemRequest request, ISender sender) =>
        {
            var command = request.Adapt<RemoveItemCommand>();
            var result = await sender.Send(command);
            return Results.Ok(result.Cart);
        })
        .WithDisplayName("RemoveItem")
        .WithDescription("RemoveItem")
        .WithSummary("Remove Item From Cart")
        .Produces(StatusCodes.Status200OK); ;
    }
}
