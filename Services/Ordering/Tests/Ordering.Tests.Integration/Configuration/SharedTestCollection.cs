namespace Ordering.Tests.Integration.Configuration;

[CollectionDefinition("OrderingTests")]
public class SharedTestCollection : ICollectionFixture<CustomWebApplicationFactory>
{
}
