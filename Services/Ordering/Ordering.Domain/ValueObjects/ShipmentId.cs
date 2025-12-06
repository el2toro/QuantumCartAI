namespace Ordering.Domain.ValueObjects;

public record ShipmentId(Guid Value)
{
    public static ShipmentId Create() => new(Guid.NewGuid());
    public static ShipmentId Of(string value) => new(Guid.Parse(value));
}
