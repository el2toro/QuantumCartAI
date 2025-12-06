namespace Ordering.Domain.ValueObjects;

public record PaymentId(Guid Value)
{
    public static PaymentId Create() => new(Guid.NewGuid());
    public static PaymentId Of(string value) => new(Guid.Parse(value));
}
