using BuildingBlocks.CQRS;
using Catalog.API.DTOs;
using Catalog.API.Repositories;

namespace Catalog.API.Categories.GetCategories;

public record GetCategoriesQuery() : IQuery<GetCategoriesResult>;
public record GetCategoriesResult(IEnumerable<CategoryDto> Categories);
public class GetCategoriesHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
{
    public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetCategoriesAsync(cancellationToken);
        var result = categories.Adapt<IEnumerable<CategoryDto>>();

        return new GetCategoriesResult(result);
    }
}
