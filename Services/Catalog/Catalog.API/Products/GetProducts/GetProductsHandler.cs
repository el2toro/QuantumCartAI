using Catalog.API.Data;
using Catalog.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IRequest<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);
public class GetProductsHandler(ProductDbContext productDbContext) : IRequestHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productDbContext.Products.ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}
