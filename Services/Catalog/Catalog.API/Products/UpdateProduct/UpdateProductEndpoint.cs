namespace Catalog.API.Products.UpdateProduct;

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("catalog/products", async (ProductDto request, ISender sender) =>
        {
            var result = await sender.Send(new UpdateProductCommand(request));

            return Results.Ok(result.Product);
        })
     .WithDisplayName("UpdateProduct")
     .WithDescription("UpdateProduct")
     .Produces(StatusCodes.Status200OK)
     .WithSummary("Update Product");
    }
}
