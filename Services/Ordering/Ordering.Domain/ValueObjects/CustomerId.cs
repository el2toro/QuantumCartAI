namespace Ordering.Domain.ValueObjects;

public record CustomerId(Guid Value)
{
    public static CustomerId Create() => new(Guid.NewGuid());
    public static CustomerId Of(string value) => new(Guid.Parse(value));
}