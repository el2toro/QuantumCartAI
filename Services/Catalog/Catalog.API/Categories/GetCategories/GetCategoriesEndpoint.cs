namespace Catalog.API.Categories.GetCategories;

public class GetCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("categories", async (ISender sender) =>
        {
            var response = await sender.Send(new GetCategoriesQuery());

            return Results.Ok(response.Categories);
        })
       .WithDisplayName("GetCategories")
       .WithDescription("GetCategories")
       .Produces(StatusCodes.Status200OK)
       .WithSummary("Get Categories");
    }
}
