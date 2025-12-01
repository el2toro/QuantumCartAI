namespace Catalog.API.Products.DeleteProduct;

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("catalog/products/{productId}", async (Guid productId, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(productId));

            return Results.NoContent();
        })
       .WithDisplayName("DeleteProduct")
       .WithDescription("DeleteProduct")
       .Produces(StatusCodes.Status204NoContent)
       .WithSummary("Delete Product");
    }
}
