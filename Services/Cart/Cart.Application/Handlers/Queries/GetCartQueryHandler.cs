using BuildingBlocks.CQRS;
using Cart.Application.Dtos;
using Cart.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using QuantumCartAI.Shared.Infrastructure.AspNetCore.Session;
using System.Text.Json;

namespace Cart.Application.Handlers.Queries;

public record GetCartQuery(Guid CustomerId) : IQuery<GetCartResult>;
public record GetCartResult(CartDto Cart);
public class GetCartQueryHandler(IDistributedCache distributedCache,
    IDiscountGrpcService discountGrpcService,
    CurrentSession currentSession)
    : IQueryHandler<GetCartQuery, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        var sessionId = currentSession.IsAuthenticated
                      ? currentSession.AuthenticatedUserId
                      : currentSession.AnonymousId;

        var cartString = await distributedCache.GetStringAsync(sessionId.ToString()!, token: cancellationToken);

        if (string.IsNullOrEmpty(cartString))
        {
            var customerId = currentSession.IsAuthenticated ? currentSession.AuthenticatedUserId : currentSession.AnonymousId;
            return new GetCartResult(new CartDto(Guid.NewGuid(), customerId, new List<CartItemDto>(), 0, 0));
        }

        var cart = JsonSerializer.Deserialize<CartDto>(cartString);


        //TODO: move the total calculation logic to the domain
        foreach (var item in cart?.CartItems!)
        {
            var discounts = await discountGrpcService.GetProductDiscounts(item.ProductId);

            if (discounts.Any())
            {
                foreach (var discount in discounts)
                {
                   // item.DiscountedPrice = item.Price - (item.Price * (decimal)(discount.Amount / 100));
                }
            }

            // cart.Total = cart.CartItems.Select(i => ((i.DiscountedPrice != 0) ? i.DiscountedPrice : i.Price) * item.Quantity).Sum();
        }

        return new GetCartResult(cart!);
    }
}
