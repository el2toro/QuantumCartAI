namespace Ordering.Domain.ValueObjects;

public record OrderItemId(Guid Value)
{
    public static OrderItemId Create() => new(Guid.NewGuid());
    public static OrderItemId From(string value) => new(Guid.Parse(value));
}
