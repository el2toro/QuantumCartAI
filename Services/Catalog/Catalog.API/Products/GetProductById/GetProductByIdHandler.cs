namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(ProductDto Product);
public class GetProductByIdHandler(IProductRepository productRepository)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await productRepository
            .GetProductById(query.ProductId, cancellationToken)
            ?? throw new ProductNotFoundException(query.ProductId.ToString());

        var result = product.Adapt<ProductDto>();

        return new GetProductByIdResult(result);
    }
}
