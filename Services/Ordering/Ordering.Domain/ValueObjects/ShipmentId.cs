namespace Ordering.Domain.ValueObjects;

public record ShipmentId(Guid Value)
{
    public static ShipmentId New() => new(Guid.NewGuid());
    public static ShipmentId From(string value) => new(Guid.Parse(value));
}
