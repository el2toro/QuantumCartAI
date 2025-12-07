using Ordering.Domain.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Events;

public class OrderItemRemovedEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public ProductId ProductId { get; }

    public OrderItemRemovedEvent(OrderId orderId, ProductId productId)
    {
        OrderId = orderId;
        ProductId = productId;
    }
}
