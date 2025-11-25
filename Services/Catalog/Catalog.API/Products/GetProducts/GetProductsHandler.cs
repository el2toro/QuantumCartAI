using BuildingBlocks.CQRS;
using Catalog.API.Interfaces;

namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : ICommand<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);
public class GetProductsHandler(IProductRepository productRepository)
    : ICommandHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductsAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}
