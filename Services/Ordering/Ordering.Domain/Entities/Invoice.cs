using Ordering.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Ordering.Domain.Entities;

public class Invoice : Entity
{
    public InvoiceId Id { get; private set; }
    public OrderId OrderId { get; private set; }
    public string InvoiceNumber { get; private set; }
    public decimal Amount { get; private set; }
    public decimal TaxAmount { get; private set; }
    public Currency Currency { get; private set; }
    public InvoiceStatus Status { get; private set; }
    public DateTime IssueDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? PaidDate { get; private set; }
    public string Notes { get; private set; }

    private readonly List<InvoiceLineItem> _lineItems = new();
    public IReadOnlyCollection<InvoiceLineItem> LineItems => _lineItems.AsReadOnly();

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
            Id = InvoiceId.New(),
            OrderId = orderId,
            InvoiceNumber = invoiceNumber,
            Amount = amount,
            TaxAmount = taxAmount,
            Currency = currency,
            Status = InvoiceStatus.Draft,
            IssueDate = DateTime.UtcNow,
            DueDate = dueDate
        };
    }

    public void AddLineItem(string description, decimal quantity, decimal unitPrice, decimal taxRate)
    {
        var lineItem = new InvoiceLineItem(description, quantity, unitPrice, taxRate);
        _lineItems.Add(lineItem);
    }

    public void Issue()
    {
        if (Status != InvoiceStatus.Draft)
            //  throw new OrderingDomainException("Only draft invoices can be issued");

            Status = InvoiceStatus.Issued;
        // AddDomainEvent(new InvoiceIssuedEvent(Id, OrderId, Amount));
    }

    public void MarkAsSent()
    {
        if (Status != InvoiceStatus.Issued)
            //   throw new OrderingDomainException("Only issued invoices can be marked as sent");

            Status = InvoiceStatus.Sent;
    }

    public void MarkAsPaid(DateTime paidDate)
    {
        if (Status == InvoiceStatus.Cancelled)
            //   throw new OrderingDomainException("Cancelled invoices cannot be marked as paid");

            Status = InvoiceStatus.Paid;
        PaidDate = paidDate;

        // AddDomainEvent(new InvoicePaidEvent(Id, OrderId, Amount, paidDate));
    }

    public void MarkAsOverdue()
    {
        if (Status != InvoiceStatus.Sent && Status != InvoiceStatus.Issued)
            //       throw new OrderingDomainException("Only issued or sent invoices can be marked as overdue");

            Status = InvoiceStatus.Overdue;
    }

    public void Cancel(string reason)
    {
        if (Status == InvoiceStatus.Paid)
            //     throw new OrderingDomainException("Paid invoices cannot be cancelled");

            Status = InvoiceStatus.Cancelled;
        Notes = reason;

        //  AddDomainEvent(new InvoiceCancelledEvent(Id, OrderId, reason));
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
