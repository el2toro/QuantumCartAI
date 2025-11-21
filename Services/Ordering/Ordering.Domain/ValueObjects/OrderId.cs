namespace Ordering.Domain.ValueObjects;

public record OrderId(Guid Value)
{
    public static OrderId New() => new(Guid.NewGuid());
    public static OrderId From(string value) => new(Guid.Parse(value));
}
