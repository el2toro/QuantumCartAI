using BuildingBlocks.CQRS;
using Cart.Application.Dtos;
using Cart.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Cart.Application.Handlers.Queries;

public record GetCartQuery(Guid CustomerId) : IQuery<GetCartResult>;
public record GetCartResult(CartDto Cart);
public class GetCartQueryHandler(IDistributedCache distributedCache,
    IDiscountGrpcService discountGrpcService)
    : IQueryHandler<GetCartQuery, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        var cartString = await distributedCache.GetStringAsync(query.CustomerId.ToString());
        if (cartString == null)
            throw new ArgumentNullException(nameof(cartString));

        var cart = JsonSerializer.Deserialize<CartDto>(cartString);


        //TODO: move the total calculation logic to the domain
        foreach (var item in cart?.CartItems!)
        {
            var discounts = await discountGrpcService.GetProductDiscounts(item.ProductId);

            if (discounts.Any())
            {
                foreach (var discount in discounts)
                {
                    item.DiscountedPrice = item.Price - (item.Price * (decimal)(discount.Amount / 100));
                }
            }

            cart.Total = cart.CartItems.Select(i => ((i.DiscountedPrice != 0) ? i.DiscountedPrice : i.Price) * item.Quantity).Sum();
        }

        return new GetCartResult(cart!);
    }
}
