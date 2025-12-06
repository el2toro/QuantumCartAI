using Ordering.Domain.ValueObjects;
using System.Collections.ObjectModel;
using System.Globalization;

public sealed class Currency : ValueObject
{
    // Static instances for commonly used currencies
    public static readonly Currency USD = new("USD", "$", "US Dollar", 2);
    public static readonly Currency EUR = new("EUR", "€", "Euro", 2);
    public static readonly Currency GBP = new("GBP", "£", "British Pound", 2);
    public static readonly Currency JPY = new("JPY", "¥", "Japanese Yen", 0);
    public static readonly Currency CAD = new("CAD", "C$", "Canadian Dollar", 2);
    public static readonly Currency AUD = new("AUD", "A$", "Australian Dollar", 2);
    public static readonly Currency CNY = new("CNY", "¥", "Chinese Yuan", 2);
    public static readonly Currency INR = new("INR", "₹", "Indian Rupee", 2);
    public static readonly Currency BRL = new("BRL", "R$", "Brazilian Real", 2);
    public static readonly Currency RUB = new("RUB", "₽", "Russian Ruble", 2);
    public static readonly Currency MXN = new("MXN", "$", "Mexican Peso", 2);

    // Properties
    public string Code { get; }           // ISO 4217 code
    public string Symbol { get; }         // Currency symbol
    public string Name { get; }           // Currency name
    public int DecimalPlaces { get; }     // Number of decimal places to display

    // All supported currencies (readonly collection)
    private static readonly ReadOnlyDictionary<string, Currency> _currencies;

    // Static constructor to initialize currencies
    static Currency()
    {
        var currencies = new Dictionary<string, Currency>
        {
            { USD.Code, USD },
            { EUR.Code, EUR },
            { GBP.Code, GBP },
            { JPY.Code, JPY },
            { CAD.Code, CAD },
            { AUD.Code, AUD },
            { CNY.Code, CNY },
            { INR.Code, INR },
            { BRL.Code, BRL },
            { RUB.Code, RUB },
            { MXN.Code, MXN }
        };

        _currencies = new ReadOnlyDictionary<string, Currency>(currencies);
    }

    // Private constructor - use factory methods
    private Currency(string code, string symbol, string name, int decimalPlaces)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Currency code cannot be empty", nameof(code));

        if (code.Length != 3)
            throw new ArgumentException("Currency code must be 3 characters", nameof(code));

        if (decimalPlaces < 0 || decimalPlaces > 4)
            throw new ArgumentException("Decimal places must be between 0 and 4", nameof(decimalPlaces));

        Code = code.ToUpperInvariant();
        Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DecimalPlaces = decimalPlaces;
    }

    // Factory method to create from code
    public static Currency FromCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Currency code cannot be empty", nameof(code));

        var normalizedCode = code.Trim().ToUpperInvariant();

        if (_currencies.TryGetValue(normalizedCode, out var currency))
            return currency;

        throw new UnsupportedCurrencyException($"Currency '{code}' is not supported");
    }

    // Try pattern for safe creation
    public static bool TryFromCode(string code, out Currency currency)
    {
        currency = null;

        if (string.IsNullOrWhiteSpace(code))
            return false;

        var normalizedCode = code.Trim().ToUpperInvariant();
        return _currencies.TryGetValue(normalizedCode, out currency);
    }

    // Check if currency is supported
    public static bool IsSupported(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return false;

        var normalizedCode = code.Trim().ToUpperInvariant();
        return _currencies.ContainsKey(normalizedCode);
    }

    // Get all supported currencies
    public static IReadOnlyCollection<Currency> GetAllCurrencies() => _currencies.Values;

    // Format amount with this currency
    public string Format(decimal amount, bool includeSymbol = true)
    {
        var format = $"N{DecimalPlaces}";
        var formattedAmount = amount.ToString(format, CultureInfo.InvariantCulture);

        if (includeSymbol)
        {
            return Symbol.StartsWith("$") || Symbol.StartsWith("€") || Symbol.StartsWith("£")
                ? $"{Symbol}{formattedAmount}"  // Prefix symbols
                : $"{formattedAmount}{Symbol}"; // Suffix symbols
        }

        return formattedAmount;
    }

    // Convert amount between currencies (simplified - would use external service in real app)
    public decimal ConvertTo(decimal amount, Currency targetCurrency, decimal exchangeRate)
    {
        if (this == targetCurrency)
            return amount;

        return amount * exchangeRate;
    }

    // Round amount according to currency rules
    public decimal Round(decimal amount, MidpointRounding rounding = MidpointRounding.ToEven)
    {
        var multiplier = (decimal)Math.Pow(10, DecimalPlaces);
        return Math.Round(amount * multiplier, rounding) / multiplier;
    }

    // Override equality comparison
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }

    // Implicit conversion to string
    public static implicit operator string(Currency currency) => currency?.Code;

    // Explicit conversion from string
    public static explicit operator Currency(string code) => FromCode(code);

    // Override ToString
    public override string ToString() => Code;

    // Parse method
    public static Currency Parse(string code) => FromCode(code);

    // Helper method for money calculations
    public static Money CreateMoney(decimal amount, string currencyCode)
    {
        var currency = FromCode(currencyCode);
        return new Money(amount, currency);
    }
}

// Supporting Money class for type-safe monetary calculations
public sealed class Money : ValueObject
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    public Money(decimal amount, Currency currency)
    {
        if (amount < 0)
            throw new ArgumentException("Money amount cannot be negative", nameof(amount));

        Amount = amount;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
    }

    // Factory method
    public static Money Of(decimal amount, string currencyCode)
    {
        var currency = Currency.FromCode(currencyCode);
        return new Money(amount, currency);
    }

    // Arithmetic operations with same currency
    public Money Add(Money other)
    {
        ValidateSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        ValidateSameCurrency(other);
        if (other.Amount > Amount)
            throw new InvalidOperationException("Insufficient funds");

        return new Money(Amount - other.Amount, Currency);
    }

    public Money Multiply(decimal multiplier)
    {
        if (multiplier < 0)
            throw new ArgumentException("Multiplier cannot be negative", nameof(multiplier));

        return new Money(Amount * multiplier, Currency);
    }

    public Money Divide(decimal divisor)
    {
        if (divisor <= 0)
            throw new ArgumentException("Divisor must be positive", nameof(divisor));

        return new Money(Amount / divisor, Currency);
    }

    public Money ApplyPercentage(decimal percentage)
    {
        if (percentage < 0 || percentage > 100)
            throw new ArgumentException("Percentage must be between 0 and 100", nameof(percentage));

        return new Money(Amount * (percentage / 100m), Currency);
    }

    // Comparison
    public bool IsGreaterThan(Money other)
    {
        ValidateSameCurrency(other);
        return Amount > other.Amount;
    }

    public bool IsLessThan(Money other)
    {
        ValidateSameCurrency(other);
        return Amount < other.Amount;
    }

    public bool IsZero() => Amount == 0;

    // Formatting
    public string Format(bool includeSymbol = true) => Currency.Format(Amount, includeSymbol);

    // Validation
    private void ValidateSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot operate on different currencies: {Currency} and {other.Currency}");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    // Operator overloads
    public static Money operator +(Money left, Money right) => left.Add(right);
    public static Money operator -(Money left, Money right) => left.Subtract(right);
    public static Money operator *(Money money, decimal multiplier) => money.Multiply(multiplier);
    public static Money operator /(Money money, decimal divisor) => money.Divide(divisor);

    public static bool operator >(Money left, Money right) => left.IsGreaterThan(right);
    public static bool operator <(Money left, Money right) => left.IsLessThan(right);
    public static bool operator >=(Money left, Money right) => !left.IsLessThan(right);
    public static bool operator <=(Money left, Money right) => !left.IsGreaterThan(right);

    public override string ToString() => $"{Currency.Code} {Amount:N2}";
}

// Custom exception for unsupported currencies
public class UnsupportedCurrencyException : Exception
{
    public UnsupportedCurrencyException(string message) : base(message) { }
}

// EF Core Value Converter for Money
//public class MoneyConverter : ValueConverter<Money, string>
//{
//    public MoneyConverter() : base(
//        money => $"{money.Amount}|{money.Currency.Code}",
//        str => ConvertFromString(str),
//        null)
//    {
//    }

//    private static Money ConvertFromString(string str)
//    {
//        if (string.IsNullOrEmpty(str))
//            return null;

//        var parts = str.Split('|');
//        if (parts.Length != 2)
//            throw new FormatException("Invalid money format");

//        var amount = decimal.Parse(parts[0], CultureInfo.InvariantCulture);
//        var currency = Currency.FromCode(parts[1]);

//        return new Money(amount, currency);
//    }
//}