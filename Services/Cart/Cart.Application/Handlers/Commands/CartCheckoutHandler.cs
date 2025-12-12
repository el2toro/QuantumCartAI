using BuildingBlocks.CQRS;
using Cart.Domain.Interfaces;

namespace Cart.Application.Handlers.Commands;

public record CartCheckoutCommand(Guid CartId) : ICommand<CartCheckoutResult>;
public record CartCheckoutResult();
public class CartCheckoutHandler(ICartRepository cartRepository) : ICommandHandler<CartCheckoutCommand, CartCheckoutResult>
{
    public async Task<CartCheckoutResult> Handle(CartCheckoutCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(command.CartId);

        return new CartCheckoutResult();
    }
}
