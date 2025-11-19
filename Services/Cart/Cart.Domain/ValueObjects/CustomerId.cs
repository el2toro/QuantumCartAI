namespace Cart.Domain.ValueObjects;

public record CustomerId(Guid Value)
{
    public static CustomerId New() => new(Guid.NewGuid());
    public static CustomerId From(string id) => new(Guid.Parse(id));
}
