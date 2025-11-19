
namespace Cart.Domain.ValueObjects;

public record ProductId(Guid Value)
{
    public static ProductId New() => new(Guid.NewGuid());
    public static ProductId From(string id) => new(Guid.Parse(id));
}
