using System.Text.RegularExpressions;

namespace Ordering.Domain.ValueObjects;

public class OrderNumber : ValueObject
{
    private const string Pattern = @"^ORD-\d{8}-\d{5}$";
    private static readonly Regex ValidationRegex = new(Pattern, RegexOptions.Compiled);

    public string Value { get; }

    private OrderNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Order number cannot be empty", nameof(value));

        if (!ValidationRegex.IsMatch(value))
            throw new ArgumentException($"Order number format is invalid. Expected: {Pattern}", nameof(value));

        if (value.Length > 20)
            throw new ArgumentException("Order number cannot exceed 20 characters", nameof(value));

        Value = value.Trim().ToUpperInvariant();
    }

    // Factory method for creating from string
    public static OrderNumber Of(string value) => new(value);

    // Factory method for generating new order numbers
    public static OrderNumber Generate()
    {
        // Format: ORD-YYYYMMDD-XXXXX
        var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
        var randomPart = GenerateRandomSequence(5);
        return new OrderNumber($"ORD-{datePart}-{randomPart}");
    }

    private static string GenerateRandomSequence(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
