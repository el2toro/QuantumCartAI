using Ordering.Domain.ValueObjects;
using SharedKernel.Common;

namespace Ordering.Domain.Events;

public class OrderItemAddedEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public ProductId ProductId { get; }
    public int Quantity { get; }
    public OrderItemAddedEvent(OrderId orderId, ProductId productId, int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }
}
