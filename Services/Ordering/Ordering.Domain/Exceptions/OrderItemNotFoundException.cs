namespace Ordering.Domain.Exceptions;

public class OrderItemNotFoundException : NotFoundException
{
    public OrderItemNotFoundException(Guid productId)
        : base($"Order item with product ID {productId} was not found.")
    {
    }
}
