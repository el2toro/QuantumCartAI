namespace Ordering.Domain.ValueObjects;

public sealed class Address : ValueObject
{
    // Properties are immutable
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }

    private Address()
    {

    }

    // Constructor - all properties set at creation
    public Address(
        string street,
        string city,
        string state,
        string country,
        string zipCode)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street cannot be empty", nameof(street));

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be empty", nameof(city));

        if (string.IsNullOrWhiteSpace(state))
            throw new ArgumentException("State cannot be empty", nameof(state));

        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be empty", nameof(country));

        if (string.IsNullOrWhiteSpace(zipCode))
            throw new ArgumentException("ZipCode cannot be empty", nameof(zipCode));

        Street = street.Trim();
        City = city.Trim();
        State = state.Trim();
        Country = country.Trim();
        ZipCode = zipCode.Trim();
    }

    // Equality is based on all property values
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return ZipCode;
    }

    // Validation methods
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Street) &&
               !string.IsNullOrWhiteSpace(City) &&
               !string.IsNullOrWhiteSpace(State) &&
               !string.IsNullOrWhiteSpace(Country) &&
               !string.IsNullOrWhiteSpace(ZipCode);
    }

    public bool IsDomestic() => Country.Equals("USA", StringComparison.OrdinalIgnoreCase);

    // Factory methods for common addresses
    public static Address CreateDomestic(
        string street,
        string city,
        string state,
        string zipCode)
    {
        return new Address(street, city, state, "USA", zipCode);
    }

    // Formatting
    public string FormatSingleLine()
    {
        var parts = new List<string> { Street };

        if (!string.IsNullOrWhiteSpace(City))
            parts.Add(City);

        if (!string.IsNullOrWhiteSpace(State))
            parts.Add(State);

        if (!string.IsNullOrWhiteSpace(ZipCode))
            parts.Add(ZipCode);

        if (!string.IsNullOrWhiteSpace(Country) && !Country.Equals("USA", StringComparison.OrdinalIgnoreCase))
            parts.Add(Country);

        return string.Join(", ", parts);
    }

    public string FormatMultiLine()
    {
        var lines = new List<string>();

        lines.Add(Street);
        lines.Add($"{City}, {State} {ZipCode}");

        if (!string.IsNullOrWhiteSpace(Country) && !Country.Equals("USA", StringComparison.OrdinalIgnoreCase))
            lines.Add(Country);

        return string.Join(Environment.NewLine, lines);
    }

    // Clone with modifications (immutable pattern)
    public Address WithStreet(string newStreet) =>
        new Address(newStreet, City, State, Country, ZipCode);

    public Address WithCity(string newCity) =>
        new Address(Street, newCity, State, Country, ZipCode);

    // Parse from string (if needed)
    public static bool TryParse(string addressString, out Address address)
    {
        address = null;

        // Simple parsing logic - customize as needed
        var parts = addressString.Split(',');
        if (parts.Length < 3)
            return false;

        try
        {
            address = new Address(
                street: parts[0].Trim(),
                city: parts[1].Trim(),
                state: parts[2].Trim(),
                country: parts.Length > 3 ? parts[3].Trim() : "USA",
                zipCode: parts.Length > 4 ? parts[4].Trim() : "");
            return true;
        }
        catch
        {
            return false;
        }
    }

    // Override ToString
    public override string ToString() => FormatSingleLine();
}
