namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryIdQuery(Guid CategoryId) : IQuery<GetProductByCategoryIdResult>;
public record GetProductByCategoryIdResult(IEnumerable<ProductDto> Products);

public class GetProductByCategoryIdHandler(IProductRepository productRepository)
    : IQueryHandler<GetProductsByCategoryIdQuery, GetProductByCategoryIdResult>
{
    public async Task<GetProductByCategoryIdResult> Handle(GetProductsByCategoryIdQuery query, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsByCategoryAsync(query.CategoryId, cancellationToken);
        var result = products.Adapt<IEnumerable<ProductDto>>();

        return new GetProductByCategoryIdResult(result);
    }
}
