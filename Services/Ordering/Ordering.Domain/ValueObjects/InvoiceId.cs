
namespace Ordering.Domain.ValueObjects;

public record InvoiceId(Guid Value)
{
    public static InvoiceId Create() => new(Guid.NewGuid());
    public static InvoiceId Of(string value) => new(Guid.Parse(value));
}
