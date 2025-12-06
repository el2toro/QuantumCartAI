using Ordering.Domain.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Events;

public class OrderConfirmedEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public CustomerId CustomerId { get; }
    public string OrderNumber { get; }
    public decimal TotalAmount { get; }

    public OrderConfirmedEvent(OrderId orderId, CustomerId customerId, string orderNumber, decimal totalAmount)
    {
        OrderId = orderId;
        CustomerId = customerId;
        OrderNumber = orderNumber;
        TotalAmount = totalAmount;
    }
}