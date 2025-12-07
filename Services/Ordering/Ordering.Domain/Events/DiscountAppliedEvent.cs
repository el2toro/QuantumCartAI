using Ordering.Domain.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Events;

public class DiscountAppliedEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public decimal DiscountAmount { get; }
    public string DiscountCode { get; }
    public DiscountAppliedEvent(OrderId orderId, decimal discountAmount, string discountCode)
    {
        OrderId = orderId;
        DiscountAmount = discountAmount;
        DiscountCode = discountCode;
    }
}
