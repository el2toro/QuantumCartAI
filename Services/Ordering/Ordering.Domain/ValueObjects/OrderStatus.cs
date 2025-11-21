namespace Ordering.Domain.ValueObjects;

public enum OrderStatus
{
    Submitted = 1,
    AwaitingPayment = 2,
    Paid = 3,
    Shipped = 4,
    Completed = 5,
    Cancelled = 6
}
