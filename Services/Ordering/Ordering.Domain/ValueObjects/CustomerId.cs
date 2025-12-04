namespace Ordering.Domain.ValueObjects;

public record CustomerId(Guid Value)
{
    public static OrderId New() => new(Guid.NewGuid());
    public static OrderId From(string value) => new(Guid.Parse(value));
}