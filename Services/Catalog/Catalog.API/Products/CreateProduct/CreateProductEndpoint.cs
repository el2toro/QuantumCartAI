namespace Catalog.API.Products.CreateProduct;

public class CreateProductEndpoint : ICarterModule
{
    public record CreateProductRequest(
        string Name,
        string Description,
        string Sku,
        string ImageFile,
        decimal Price,
        int Rating,
        List<Guid> Categories);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("catalog", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);

            return Results.Created($"catalog/products/{request.Name}", result);
        });
    }
}
