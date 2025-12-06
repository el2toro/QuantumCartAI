using MediatR;

namespace Ordering.Domain.Common;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
    Guid EventId { get; }
}

public abstract class DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; }
    public Guid EventId { get; }

    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }

    protected DomainEvent(Guid eventId, DateTime occurredOn)
    {
        EventId = eventId;
        OccurredOn = occurredOn;
    }
}

// Example domain events for Order
//public class OrderDraftCreatedEvent : DomainEvent
//{
//    public OrderId OrderId { get; }
//    public CustomerId CustomerId { get; }
//    public string OrderNumber { get; }

//    public OrderDraftCreatedEvent(OrderId orderId, CustomerId customerId, string orderNumber)
//    {
//        OrderId = orderId;
//        CustomerId = customerId;
//        OrderNumber = orderNumber;
//    }
//}

//public class OrderConfirmedEvent : DomainEvent
//{
//    public OrderId OrderId { get; }
//    public CustomerId CustomerId { get; }
//    public string OrderNumber { get; }
//    public decimal TotalAmount { get; }

//    public OrderConfirmedEvent(OrderId orderId, CustomerId customerId, string orderNumber, decimal totalAmount)
//    {
//        OrderId = orderId;
//        CustomerId = customerId;
//        OrderNumber = orderNumber;
//        TotalAmount = totalAmount;
//    }
//}

//public class OrderItemAddedEvent : DomainEvent
//{
//    public OrderId OrderId { get; }
//    public ProductId ProductId { get; }
//    public int Quantity { get; }

//    public OrderItemAddedEvent(OrderId orderId, ProductId productId, int quantity)
//    {
//        OrderId = orderId;
//        ProductId = productId;
//        Quantity = quantity;
//    }
//}