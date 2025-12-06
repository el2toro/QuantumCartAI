namespace Ordering.Domain.ValueObjects;

public record AddressId(Guid Value)
{
    public static OrderId New() => new(Guid.NewGuid());
    public static OrderId Of(string value) => new(Guid.Parse(value));
}
