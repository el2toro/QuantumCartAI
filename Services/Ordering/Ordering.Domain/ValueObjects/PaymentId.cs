namespace Ordering.Domain.ValueObjects;

public record PaymentId(Guid Value)
{
    public static PaymentId New() => new(Guid.NewGuid());
    public static PaymentId From(string value) => new(Guid.Parse(value));
}
