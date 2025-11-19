namespace Cart.Domain.Interfaces;

public interface ICartRepository
{
    Task<Entities.Cart?> GetByIdAsync(Guid cartId);
    Task SaveAsync(Entities.Cart cart);
}
