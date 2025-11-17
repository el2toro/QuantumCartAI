namespace Cart.Domain.ValueObjects;

public record SkuId(string Value)
{
    public static SkuId From(string id) => new(id);
}
