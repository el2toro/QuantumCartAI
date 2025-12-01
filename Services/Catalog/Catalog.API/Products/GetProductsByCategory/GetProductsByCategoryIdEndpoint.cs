namespace Catalog.API.Products.GetProductsByCategory;

public class GetProductsByCategoryIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("catalog/products/category/{categoryId}", async (Guid categoryId, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByCategoryIdQuery(categoryId));

            return Results.Ok(result.Products);
        })
       .WithDisplayName("GetProductsByCategoryId")
       .WithDescription("GetProductsByCategoryId")
       .Produces(StatusCodes.Status200OK)
       .WithSummary("Get Products By Category Id");
    }
}
