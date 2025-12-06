using Ordering.Domain.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Events;

public class InvoiceIssuedEvent : DomainEvent
{
    public InvoiceId InvoiceId { get; }
    public OrderId OrderId { get; }
    public decimal Amount { get; }

    public InvoiceIssuedEvent(InvoiceId invoiceId, OrderId orderId, decimal amount)
    {
        InvoiceId = invoiceId;
        OrderId = orderId;
        Amount = amount;
    }
}
