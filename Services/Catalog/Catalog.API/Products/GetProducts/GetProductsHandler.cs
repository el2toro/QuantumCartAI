namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(IEnumerable<Guid> CategoryIds) : IRequest<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);
public class GetProductsHandler(ProductDbContext productDbContext) : IRequestHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productDbContext.Products.ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}
