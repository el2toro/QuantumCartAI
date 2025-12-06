namespace Ordering.Domain.ValueObjects;

public record ProductId(Guid Value)
{
    public static ProductId Create() => new(Guid.NewGuid());
    public static ProductId Of(string value) => new(Guid.Parse(value));
}
