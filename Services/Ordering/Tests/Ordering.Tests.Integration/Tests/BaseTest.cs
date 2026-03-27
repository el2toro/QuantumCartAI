using Ordering.Tests.Integration.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ordering.Tests.Integration.Tests;

public abstract class BaseTest(CustomWebApplicationFactory webFactory)
{
    protected CustomWebApplicationFactory WebFactory => webFactory;

    protected JsonSerializerOptions JsonSerializerOptions => new()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    protected async Task CreateOrderDraft()
    {

    }
}
