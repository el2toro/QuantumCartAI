using BuildingBlocks.CQRS;
using Cart.Application.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Cart.Application.Handlers.Queries;

public record GetCartQuery(Guid CustomerId) : IQuery<GetCartResult>;
public record GetCartResult(CartDto Cart);
public class GetCartQueryHandler(IDistributedCache distributedCache) : IQueryHandler<GetCartQuery, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        var cartString = await distributedCache.GetStringAsync(query.CustomerId.ToString());
        if (cartString == null)
            throw new ArgumentNullException(nameof(cartString));

        var cart = JsonSerializer.Deserialize<CartDto>(cartString);

        return new GetCartResult(cart!);
    }
}
