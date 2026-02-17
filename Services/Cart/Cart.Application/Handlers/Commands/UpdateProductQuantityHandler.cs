using BuildingBlocks.CQRS;
using Cart.Application.Dtos;
using Cart.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;

namespace Cart.Application.Handlers.Commands;

public record UpdateProductQuantityCommand(Guid? CustomerId, Guid? CartId, Guid ProductId, int Quantity)
    : ICommand<UpdateProductQuantityResult>;
public record UpdateProductQuantityResult(CartDto Cart);
public class UpdateProductQuantityHandler(IDistributedCache distributedCache)
    : ICommandHandler<UpdateProductQuantityCommand, UpdateProductQuantityResult>
{
    public async Task<UpdateProductQuantityResult> Handle(UpdateProductQuantityCommand command, CancellationToken cancellationToken)
    {
        var cart = await distributedCache.GetStringAsync(command.CustomerId.ToString()!, cancellationToken);
        return new UpdateProductQuantityResult(new CartDto(Guid.NewGuid(), Guid.NewGuid(), new List<CartItemDto>(), 0, 0));
    }
}

