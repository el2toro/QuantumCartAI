namespace Cart.Domain.ValueObjects;

public record CartId(Guid Value)
{
    public static CartId New() => new(Guid.NewGuid());
    public static CartId From(string id) => new(Guid.Parse(id));
}
