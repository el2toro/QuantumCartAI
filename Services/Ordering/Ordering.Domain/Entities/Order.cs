using Ordering.Domain.Common;
using Ordering.Domain.Events;
using Ordering.Domain.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities;

public class Order : AggregateRoot<OrderId>
{
    // Private collections for relationships
    private readonly List<OrderItem> _orderItems = new();
    private readonly List<Payment> _payments = new();
    private readonly List<Shipment> _shipments = new();
    private readonly List<Invoice> _invoices = new();
    private readonly List<OrderStatusHistory> _statusHistory = new();

    // Basic Properties
    public CustomerId CustomerId { get; private set; }
    public OrderNumber OrderNumber { get; private set; }
    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }
    public OrderStatus Status { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public decimal TotalAmount { get; private set; }
    public Currency Currency { get; private set; }
    public DateTime OrderDate { get; private set; }
    public DateTime? PaidDate { get; private set; }
    public DateTime? ShippedDate { get; private set; }
    public DateTime? DeliveredDate { get; private set; }
    public DateTime? CancelledDate { get; private set; }
    public string CancellationReason { get; private set; }
    public decimal AppliedDiscount { get; private set; }
    public string DiscountCode { get; private set; }
    public decimal ShippingCost { get; private set; }
    // public string ShippingMethod { get; private set; } = "standard";
    // public string PaymentMethod { get; private set; }
    //public string OrderType { get; private set; } = "retail";
    // public string Priority { get; private set; } = "normal";
    //public DateTime? ExpectedDeliveryDate { get; private set; }
    public string CustomerNotes { get; private set; }
    //  public string InternalNotes { get; private set; }

    // Audit
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Metadata
    // public Dictionary<string, object> Metadata { get; private set; } = new();

    // ============================================
    // NAVIGATION PROPERTIES (Relationships)
    // ============================================

    // One-to-Many: Order -> OrderItems
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    // One-to-Many: Order -> Payments
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    // One-to-Many: Order -> Shipments
    public IReadOnlyCollection<Shipment> Shipments => _shipments.AsReadOnly();

    // One-to-Many: Order -> Invoices
    public IReadOnlyCollection<Invoice> Invoices => _invoices.AsReadOnly();

    // One-to-Many: Order -> StatusHistory
    public IReadOnlyCollection<OrderStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    // ============================================
    // CONSTRUCTOR & FACTORY METHOD
    // ============================================

    // Private constructor for EF Core
    private Order() : base() { }

    public static Order CreateDraft(
        CustomerId customerId,
        OrderNumber orderNumber,
        Address shippingAddress,
        Address billingAddress,
        Currency currency,
        string customerNotes)
    {
        if (customerId == null)
            throw new ArgumentNullException(nameof(customerId));

        var order = new Order
        {
            Id = OrderId.Create(),
            CustomerId = customerId,
            OrderNumber = orderNumber,
            ShippingAddress = shippingAddress ?? throw new ArgumentNullException(nameof(shippingAddress)),
            BillingAddress = billingAddress ?? shippingAddress,
            Status = OrderStatus.Draft,
            PaymentStatus = PaymentStatus.Pending,
            Currency = currency ?? Currency.USD,
            OrderDate = DateTime.UtcNow,
            TotalAmount = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CustomerNotes = customerNotes,
            CancellationReason = "none",
            AppliedDiscount = 10,
            DiscountCode = "discount-1234",

        };

        // Add initial status history
        order.AddStatusHistory(OrderStatus.Draft, "system", "Order draft created");

        // Raise domain event
        order.AddDomainEvent(new OrderDraftCreatedEvent(
            order.Id,
            customerId,
            orderNumber.Value));

        return order;
    }

    // ============================================
    // DOMAIN METHODS - ORDER ITEMS
    // ============================================

    public void AddOrderItem(
        ProductId productId,
        string productName,
        string productImageUrl,
        string productSku,
        decimal unitPrice,
        int quantity,
        decimal? discount)
    {
        if (Status != OrderStatus.Draft)
            throw new OrderingDomainException("Cannot modify order in current state");

        var existingItem = _orderItems.FirstOrDefault(i => i.ProductId == productId)!;

        if (existingItem is not null)
        {
            existingItem.AddUnits(quantity);
        }
        else
        {
            var orderItem = OrderItem.Create(
                productId,
                productName,
                productImageUrl,
                productSku,
                unitPrice,
                quantity,
                discount ?? 0);
            _orderItems.Add(orderItem);
        }

        RecalculateTotalAmount();
        AddDomainEvent(new OrderItemAddedEvent(Id, productId, quantity));
    }

    public void RemoveOrderItem(ProductId productId)
    {
        if (Status != OrderStatus.Draft)
            throw new OrderingDomainException("Cannot modify order in current state");

        var item = _orderItems.FirstOrDefault(i => i.ProductId == productId)!;
        if (item is not null)
        {
            _orderItems.Remove(item);
            RecalculateTotalAmount();

            AddDomainEvent(new OrderItemRemovedEvent(Id, productId));
        }
    }

    // ============================================
    // DOMAIN METHODS - PAYMENTS
    // ============================================

    public Payment AddPayment(
        decimal amount,
        string paymentMethod,
        string gatewayTransactionId = default!,
        string cardLastFour = default!)
    {
        if (amount <= 0)
            throw new OrderingDomainException("Payment amount must be positive");

        var payment = Payment.Create(
            Id,
            amount,
            Currency,
            paymentMethod,
            gatewayTransactionId,
            cardLastFour);

        _payments.Add(payment);

        UpdatePaymentStatus();
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new PaymentAddedEvent(Id, payment.Id, amount));

        return payment;
    }

    public void MarkPaymentAsPaid(PaymentId paymentId, string gatewayResponse = default!)
    {
        var payment = _payments.FirstOrDefault(p => p.Id == paymentId);
        //if (payment == null)
        //    throw new PaymentNotFoundException(paymentId);

        payment?.MarkAsPaid(gatewayResponse);
        UpdatePaymentStatus();

        if (PaymentStatus == PaymentStatus.Paid)
        {
            PaidDate = DateTime.UtcNow;
            Status = OrderStatus.Confirmed;

            AddStatusHistory(OrderStatus.Confirmed, "system", "Payment confirmed");
            AddDomainEvent(new OrderConfirmedEvent(Id, CustomerId, OrderNumber.Value, TotalAmount));
        }
    }

    // ============================================
    // DOMAIN METHODS - SHIPMENTS
    // ============================================

    public Shipment AddShipment(
        string trackingNumber,
        string carrier,
        decimal shippingCost,
        string serviceType = "Standard")
    {
        if (Status != OrderStatus.Confirmed && Status != OrderStatus.Processing)
            throw new OrderingDomainException("Cannot add shipment to order in current state");

        var shipment = Shipment.Create(
            Id,
            trackingNumber,
            carrier,
            shippingCost,
            ShippingAddress,
            serviceType);

        _shipments.Add(shipment);
        ShippingCost = shippingCost;

        if (Status == OrderStatus.Confirmed || Status == OrderStatus.Processing)
        {
            Status = OrderStatus.Shipped;
            ShippedDate = DateTime.UtcNow;

            AddStatusHistory(OrderStatus.Shipped, "system", $"Shipped via {carrier}");
            AddDomainEvent(new OrderShippedEvent(Id, CustomerId, trackingNumber, carrier, DateTime.UtcNow));
        }

        RecalculateTotalAmount();
        UpdatedAt = DateTime.UtcNow;

        return shipment;
    }

    public void MarkShipmentAsDelivered(ShipmentId shipmentId, DateTime deliveredDate, string deliveryNotes = default!)
    {
        var shipment = _shipments.FirstOrDefault(s => s.Id == shipmentId);
        //if (shipment == null)
        //    throw new ShipmentNotFoundException(shipmentId);

        shipment?.MarkAsDelivered(deliveredDate, deliveryNotes);

        if (_shipments.All(s => s.Status == ShipmentStatus.Delivered))
        {
            Status = OrderStatus.Delivered;
            DeliveredDate = deliveredDate;

            AddStatusHistory(OrderStatus.Delivered, "system", "All shipments delivered");
            AddDomainEvent(new OrderDeliveredEvent(Id, CustomerId, deliveredDate));
        }

        UpdatedAt = DateTime.UtcNow;
    }

    // ============================================
    // DOMAIN METHODS - INVOICES
    // ============================================

    public Invoice CreateInvoice(string invoiceNumber, DateTime dueDate)
    {
        if (Status != OrderStatus.Confirmed && Status != OrderStatus.Processing && Status != OrderStatus.Shipped)
            throw new OrderingDomainException("Cannot create invoice for order in current state");

        var invoice = Invoice.Create(
            Id,
            invoiceNumber,
            TotalAmount,
            CalculateTaxAmount(),
            Currency,
            dueDate);

        _invoices.Add(invoice);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new InvoiceIssuedEvent(invoice.Id, Id, TotalAmount));

        return invoice;
    }

    // ============================================
    // DOMAIN METHODS - STATUS & GENERAL
    // ============================================

    public void Confirm()
    {
        if (Status != OrderStatus.Draft)
            throw new OrderingDomainException("Only draft orders can be confirmed");

        if (_orderItems.Count == 0)
            throw new OrderingDomainException("Cannot confirm empty order");

        Status = OrderStatus.Confirmed;
        UpdatedAt = DateTime.UtcNow;

        AddStatusHistory(OrderStatus.Confirmed, "customer", "Order confirmed");
        AddDomainEvent(new OrderConfirmedEvent(Id, CustomerId, OrderNumber.Value, TotalAmount));
    }

    public void Cancel(string reason)
    {
        if (Status == OrderStatus.Delivered || Status == OrderStatus.Shipped)
            throw new OrderingDomainException("Cannot cancel shipped or delivered orders");

        Status = OrderStatus.Cancelled;
        CancelledDate = DateTime.UtcNow;
        CancellationReason = reason ?? "Customer request";
        UpdatedAt = DateTime.UtcNow;

        AddStatusHistory(OrderStatus.Cancelled, "system", $"Order cancelled: {CancellationReason}");
        AddDomainEvent(new OrderCancelledEvent(Id, CustomerId, CancellationReason, TotalAmount));
    }

    public void StartProcessing()
    {
        if (Status != OrderStatus.Confirmed || PaymentStatus != PaymentStatus.Paid)
            throw new OrderingDomainException("Only paid confirmed orders can be processed");

        Status = OrderStatus.Processing;
        UpdatedAt = DateTime.UtcNow;

        AddStatusHistory(OrderStatus.Processing, "system", "Order processing started");
        AddDomainEvent(new OrderProcessingStartedEvent(Id, CustomerId, OrderNumber.Value));
    }

    public void ApplyDiscount(decimal discountAmount, string discountCode)
    {
        if (discountAmount < 0)
            throw new OrderingDomainException("Discount cannot be negative");

        if (discountAmount > TotalAmount)
            throw new OrderingDomainException("Discount cannot exceed order total");

        AppliedDiscount = discountAmount;
        DiscountCode = discountCode;
        RecalculateTotalAmount();
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new DiscountAppliedEvent(Id, discountAmount, discountCode));
    }

    public void SetShippingMethod(string method, decimal cost)
    {
        // ShippingMethod = method;
        ShippingCost = cost;
        RecalculateTotalAmount();
        UpdatedAt = DateTime.UtcNow;
    }

    // ============================================
    // PRIVATE HELPER METHODS
    // ============================================

    private void AddStatusHistory(OrderStatus status, string changedBy, string notes)
    {
        var statusHistory = new OrderStatusHistory(Id, status, changedBy, notes);
        _statusHistory.Add(statusHistory);
    }

    private void RecalculateTotalAmount()
    {
        var itemsTotal = _orderItems.Sum(item => item.GetTotalPrice());
        TotalAmount = itemsTotal + ShippingCost - AppliedDiscount;
    }

    private decimal CalculateTaxAmount()
    {
        // Simplified tax calculation
        // In real app, would integrate with tax service
        var taxableAmount = TotalAmount - ShippingCost;
        return taxableAmount * 0.1m; // 10% tax
    }

    private void UpdatePaymentStatus()
    {
        if (!_payments.Any())
        {
            PaymentStatus = PaymentStatus.Pending;
            return;
        }

        var totalPaid = _payments
            .Where(p => p.Status == PaymentStatus.Paid)
            .Sum(p => p.Amount);

        //var totalRefunded = _payments
        //    .Where(p => p.Status == PaymentStatus.Refunded || p.Status == PaymentStatus.PartiallyRefunded)
        //    .Sum(p => p.RefundAmount ?? 0);

        var netAmount = totalPaid; ///- totalRefunded;

        if (netAmount >= TotalAmount)
        {
            PaymentStatus = PaymentStatus.Paid;
            PaidDate = _payments
                .Where(p => p.Status == PaymentStatus.Paid)
                .Max(p => p.PaymentDate);
        }
        else if (netAmount > 0)
        {
            // PaymentStatus = PaymentStatus.PartiallyPaid;
        }
        else if (_payments.Any(p => p.Status == PaymentStatus.Failed))
        {
            PaymentStatus = PaymentStatus.Failed;
        }
        else if (_payments.Any(p => p.Status == PaymentStatus.Processing))
        {
            PaymentStatus = PaymentStatus.Processing;
        }
    }

    // ============================================
    // VALIDATION & BUSINESS RULES
    // ============================================

    public void ValidateOrder()
    {
        if (_orderItems.Count == 0)
            throw new OrderingDomainException("Order must have at least one item");

        if (TotalAmount <= 0)
            throw new OrderingDomainException("Order total must be positive");

        //if (Status == OrderStatus.Paid && PaymentStatus != PaymentStatus.Paid)
        //    throw new OrderingDomainException("Order cannot be in Paid status without successful payment");

        if (Status == OrderStatus.Shipped && !_shipments.Any())
            throw new OrderingDomainException("Order cannot be Shipped without shipments");

        if (Status == OrderStatus.Delivered && !DeliveredDate.HasValue)
            throw new OrderingDomainException("Order cannot be Delivered without delivery date");

        // Validate total amount calculation
        var calculatedTotal = _orderItems.Sum(i => i.GetTotalPrice()) +
                             ShippingCost -
                             AppliedDiscount;

        if (Math.Abs(TotalAmount - calculatedTotal) > 0.01m)
            throw new OrderingDomainException($"Order total mismatch. Expected: {calculatedTotal}, Actual: {TotalAmount}");
    }

    // ============================================
    // QUERY METHODS
    // ============================================

    public bool HasShipped() => Status == OrderStatus.Shipped || Status == OrderStatus.Delivered;

    public bool IsPaid() => PaymentStatus == PaymentStatus.Paid;

    public bool CanBeCancelled() =>
        Status != OrderStatus.Cancelled &&
        Status != OrderStatus.Delivered &&
        Status != OrderStatus.Shipped;

    public decimal GetItemsTotal() => _orderItems.Sum(i => i.GetTotalPrice());

    public int GetTotalItemsCount() => _orderItems.Sum(i => i.Quantity);

    public Payment GetLatestPayment() => _payments.OrderByDescending(p => p.PaymentDate).FirstOrDefault()!;

    public Shipment GetPrimaryShipment() => _shipments.FirstOrDefault()!;

    public Invoice GetLatestInvoice() => _invoices.OrderByDescending(i => i.IssueDate).FirstOrDefault()!;

    public OrderStatusHistory GetLatestStatusChange() =>
        _statusHistory.OrderByDescending(sh => sh.ChangedAt).FirstOrDefault()!;
}