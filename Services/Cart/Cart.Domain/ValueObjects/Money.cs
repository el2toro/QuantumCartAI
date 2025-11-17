namespace Cart.Domain.ValueObjects;

public record Money(decimal Amount, string Currency = "USD")
{
    public static Money Zero => new(0m);
    public Money Multiply(Quantity qty) => this with { Amount = Amount * qty.Value };

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Cannot add Money with different currencies.");
        return new Money(a.Amount + b.Amount, a.Currency);
    }
}
