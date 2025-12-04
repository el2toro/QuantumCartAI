
namespace Ordering.Domain.ValueObjects;

public record InvoiceId(Guid Value)
{
    public static InvoiceId New() => new(Guid.NewGuid());
    public static InvoiceId From(string value) => new(Guid.Parse(value));
}
