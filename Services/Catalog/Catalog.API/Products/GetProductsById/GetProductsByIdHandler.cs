namespace Catalog.API.Products.GetProductsById;

public record GetProductsByIdQuery(Guid[] ProductIds) : IQuery<GetProductsByIdResult>;
public record GetProductsByIdResult(IEnumerable<ProductDto> Products);
public class GetProductsByIdHandler(IProductRepository productRepository)
    : IQueryHandler<GetProductsByIdQuery, GetProductsByIdResult>
{
    public async Task<GetProductsByIdResult> Handle(GetProductsByIdQuery query, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsByIdAsync(query.ProductIds, cancellationToken);
        var result = products.Adapt<IEnumerable<ProductDto>>();

        return new GetProductsByIdResult(result);
    }
}
