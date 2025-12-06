using Ordering.Domain.Common;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities;

public class Invoice : AggregateRoot<InvoiceId>
{
    public OrderId OrderId { get; private set; }
    public string InvoiceNumber { get; private set; }
    public decimal Amount { get; private set; }
    public Currency Currency { get; private set; }
    public InvoiceStatus Status { get; private set; }
    public DateTime IssueDate { get; private set; }
    public DateTime PaidDate { get; private set; }

    // Navigation property to Order
    public Order Order { get; private set; }

    private Invoice() { }

    public static Invoice Create(
        OrderId orderId,
        string invoiceNumber,
        decimal amount,
        decimal taxAmount,
        Currency currency,
        DateTime dueDate)
    {
        return new Invoice
        {
            Id = InvoiceId.Create(),
            OrderId = orderId,
            InvoiceNumber = invoiceNumber,
            Amount = amount,
            Currency = currency,
            Status = InvoiceStatus.Issued,
            IssueDate = DateTime.UtcNow
        };
    }
}
public record InvoiceLineItem(
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal TaxRate)
{
    public decimal TotalAmount => Quantity * UnitPrice;
    public decimal TaxAmount => TotalAmount * TaxRate / 100;
    public decimal TotalWithTax => TotalAmount + TaxAmount;
}
