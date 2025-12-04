namespace Ordering.Domain.ValueObjects;

public enum OrderStatus
{
    Draft = 1,          // Order created but not confirmed
    Confirmed = 2,      // Order confirmed by customer
    Processing = 3,     // Order being processed
    ReadyForShipping = 4, // Order ready for shipping
    Shipped = 5,        // Order shipped to customer
    Delivered = 6,      // Order delivered to customer
    Cancelled = 7,      // Order cancelled
    Returned = 8,       // Order returned by customer
    Refunded = 9,       // Order refunded
    OnHold = 10,        // Order on hold for review
    FraudReview = 11    // Order under fraud review
}
