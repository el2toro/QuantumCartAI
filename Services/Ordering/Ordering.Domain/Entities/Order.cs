using Ordering.Domain.Events;
using Ordering.Domain.Exceptions;
using Ordering.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Ordering.Domain.Entities;

public class Order : Entity//, IAggregateRoot
{
    private readonly List<OrderItem> _orderItems = new();
    private readonly List<DomainEvent> _domainEvents = new();

    public OrderId Id { get; private set; }
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

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // Private constructor for EF Core
    private Order() { }

    // Factory method for creating new orders
    public static Order CreateDraft(
        CustomerId customerId,
        OrderNumber orderNumber,
        Address shippingAddress,
        Address billingAddress,
        Currency currency)
    {
        if (customerId == null)
            throw new ArgumentNullException(nameof(customerId));

        var order = new Order
        {
            Id = OrderId.New(),
            CustomerId = customerId,
            OrderNumber = orderNumber,
            ShippingAddress = shippingAddress ?? throw new ArgumentNullException(nameof(shippingAddress)),
            BillingAddress = billingAddress ?? shippingAddress,
            Status = OrderStatus.Draft,
            PaymentStatus = PaymentStatus.Pending,
            Currency = currency,
            OrderDate = DateTime.UtcNow,
            TotalAmount = 0
        };

        order.AddDomainEvent(new OrderDraftCreatedEvent(
            order.Id,
            customerId,
            orderNumber.Value));

        return order;
    }

    // Domain Behavior: Add item to order
    public void AddOrderItem(
        ProductId productId,
        string productName,
        string productImageUrl,
        decimal unitPrice,
        decimal? discount,
        int units)
    {
        //if (Status != OrderStatus.Draft)
        //    throw new OrderingDomainException("Cannot modify order in current state");

        var existingOrderItem = _orderItems
            .FirstOrDefault(o => o.ProductId == productId);

        if (existingOrderItem != null)
        {
            existingOrderItem.AddUnits(units);
        }
        else
        {
            var orderItem = OrderItem.Create(
                productId,
                productName,
                productImageUrl,
                unitPrice,
                discount ?? 0,
                units);

            _orderItems.Add(orderItem);
        }

        RecalculateTotalAmount();
    }

    // Domain Behavior: Remove item from order
    public void RemoveOrderItem(ProductId productId)
    {
        if (Status != OrderStatus.Draft)
            throw new OrderingDomainException("Cannot modify order in current state");

        var orderItem = _orderItems.FirstOrDefault(o => o.ProductId == productId);
        if (orderItem != null)
        {
            _orderItems.Remove(orderItem);
            RecalculateTotalAmount();
        }
    }

    // Domain Behavior: Update item quantity
    public void UpdateOrderItemQuantity(ProductId productId, int newQuantity)
    {
        if (Status != OrderStatus.Draft)
            throw new OrderingDomainException("Cannot modify order in current state");

        if (newQuantity <= 0)
        {
            RemoveOrderItem(productId);
            return;
        }

        var orderItem = _orderItems.FirstOrDefault(o => o.ProductId == productId);
        if (orderItem != null)
        {
            orderItem.SetNewQuantity(newQuantity);
            RecalculateTotalAmount();
        }
    }

    // Domain Behavior: Apply discount
    public void ApplyDiscount(decimal discountAmount, string discountCode)
    {
        if (discountAmount < 0)
            throw new OrderingDomainException("Discount cannot be negative");

        if (discountAmount > TotalAmount)
            throw new OrderingDomainException("Discount cannot exceed order total");

        TotalAmount -= discountAmount;

        //AddDomainEvent(new OrderDiscountAppliedEvent(
        //    Id,
        //    discountAmount,
        //    discountCode,
        //    TotalAmount));
    }

    // Domain Behavior: Confirm order (transition to confirmed state)
    public void Confirm()
    {
        if (Status != OrderStatus.Draft)
            throw new OrderingDomainException("Only draft orders can be confirmed");

        if (_orderItems.Count == 0)
            throw new OrderingDomainException("Cannot confirm empty order");

        Status = OrderStatus.Confirmed;

        //AddDomainEvent(new OrderConfirmedEvent(
        //    Id,
        //    CustomerId,
        //    OrderNumber.Value,
        //    TotalAmount));
    }

    // Domain Behavior: Mark as paid
    public void MarkAsPaid(string paymentId, DateTime paidDate)
    {
        //if (Status != OrderStatus.Confirmed)
        //    throw new OrderingDomainException("Only confirmed orders can be paid");

        PaymentStatus = PaymentStatus.Paid;
        PaidDate = paidDate;

        AddDomainEvent(new OrderPaidEvent(
            Id,
            CustomerId,
            paymentId,
            TotalAmount,
            paidDate));
    }

    // Domain Behavior: Start processing
    public void StartProcessing()
    {
        if (Status != OrderStatus.Confirmed || PaymentStatus != PaymentStatus.Paid)
            throw new OrderingDomainException("Only paid confirmed orders can be processed");

        Status = OrderStatus.Processing;

        //AddDomainEvent(new OrderProcessingStartedEvent(
        //    Id,
        //    CustomerId,
        //    OrderNumber.Value));
    }

    // Domain Behavior: Ship order
    public void Ship(string trackingNumber, string carrier, DateTime shippedDate)
    {
        if (Status != OrderStatus.Processing)
            throw new OrderingDomainException("Only processing orders can be shipped");

        Status = OrderStatus.Shipped;
        ShippedDate = shippedDate;

        AddDomainEvent(new OrderShippedEvent(
            Id,
            CustomerId,
            trackingNumber,
            carrier,
            shippedDate));
    }

    // Domain Behavior: Deliver order
    public void Deliver(DateTime deliveredDate)
    {
        if (Status != OrderStatus.Shipped)
            throw new OrderingDomainException("Only shipped orders can be delivered");

        Status = OrderStatus.Delivered;
        DeliveredDate = deliveredDate;

        AddDomainEvent(new OrderDeliveredEvent(
            Id,
            CustomerId,
            deliveredDate));
    }

    // Domain Behavior: Cancel order
    public void Cancel(string reason)
    {
        if (Status == OrderStatus.Delivered || Status == OrderStatus.Shipped)
            throw new OrderingDomainException("Cannot cancel shipped or delivered orders");

        Status = OrderStatus.Cancelled;
        CancelledDate = DateTime.UtcNow;
        CancellationReason = reason ?? "Customer request";

        AddDomainEvent(new OrderCancelledEvent(
            Id,
            CustomerId,
            CancellationReason,
            TotalAmount));
    }

    // Domain Behavior: Return order
    public void Return(string returnReason, DateTime returnDate)
    {
        if (Status != OrderStatus.Delivered)
            throw new OrderingDomainException("Only delivered orders can be returned");

        //Status = OrderStatus.Returned;

        //AddDomainEvent(new OrderReturnedEvent(
        //    Id,
        //    CustomerId,
        //    returnReason,
        //    returnDate,
        //    TotalAmount));
    }

    // Recalculate total amount
    private void RecalculateTotalAmount()
    {
        TotalAmount = _orderItems.Sum(item => item.GetTotalPrice());
    }

    // Domain event management
    private void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}