namespace Ordering.Domain.ValueObjects;

public enum PaymentStatus
{
    Pending = 1,        // Payment not yet initiated
    Processing = 2,     // Payment being processed
    Paid = 3,          // Payment successful
    Failed = 4,        // Payment failed
    Refunded = 5,      // Payment refunded
    PartiallyRefunded = 6, // Partial refund
    Disputed = 7       // Payment disputed
}
