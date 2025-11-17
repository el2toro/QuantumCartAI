using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cart.Domain.ValueObjects;

/// <summary>
/// Immutable value object for shipping address.
/// Validates format, supports US/EU differences.
/// </summary>
public record Address(
    string Street1,
    string? Street2,
    string City,
    string StateOrProvince,  // e.g., CA, Ile-de-France
    string PostalCode,
    string CountryCode,      // ISO 3166-1 alpha-2: US, FR, DE
    string? Phone = null
)
{
    // Optional: Full name (for label)
    [JsonPropertyName("fullName")]
    public string? FullName { get; init; }

    // Validation in constructor
    public Address Validate()
    {
        if (string.IsNullOrWhiteSpace(Street1)) throw new ArgumentException("Street1 required");
        if (string.IsNullOrWhiteSpace(City)) throw new ArgumentException("City required");
        if (string.IsNullOrWhiteSpace(PostalCode)) throw new ArgumentException("PostalCode required");
        if (!IsValidCountry(CountryCode)) throw new ArgumentException("Invalid country code");

        // BR-20: Basic postal format (extend per country)
        if (CountryCode == "US" && !System.Text.RegularExpressions.Regex.IsMatch(PostalCode, @"^\d{5}(-\d{4})?$"))
            throw new ArgumentException("Invalid US ZIP");

        return this;
    }

    private static bool IsValidCountry(string code) =>
        code.Length == 2 && new[] { "US", "CA", "GB", "FR", "DE", "IT", "ES", "MD" }.Contains(code.ToUpper());

    // For EventStoreDB / JSON
    public static Address FromJson(string json) =>
        JsonSerializer.Deserialize<Address>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
        ?? throw new InvalidOperationException();
}
