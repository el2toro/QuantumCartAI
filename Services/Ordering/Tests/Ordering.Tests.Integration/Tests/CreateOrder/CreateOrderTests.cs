using Ordering.Application.DTOs;
using Ordering.Application.DTOs.Requests;
using Ordering.Domain.ValueObjects;
using Ordering.Tests.Integration.Configuration;
using System.Net.Http.Json;

namespace Ordering.Tests.Integration.Tests.CreateOrder;

[Collection("OrderingTests")]
public class CreateOrderTests(CustomWebApplicationFactory webFactory)
    : BaseTest(webFactory), IAsyncLifetime
{
    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync() => Task.CompletedTask; //await WebFactory.ResetDatabaseAsync();

    [Fact]
    public async Task CreateOrderDraft_ShouldCreateNewOrder()
    {
        var request = new CreateOrderRequest
        {
            CustomerId = Guid.NewGuid(),
            CustomerNotes = "This is my first order",
            Currency = "EUR",
            ShippingAddress = new AddressDto
            {
                City = "London",
                Country = "United Kingdom",
                State = "UK",
                Street = "London street 236",
                ZipCode = "65365"
            },
            BillingAddress = new AddressDto
            {
                City = "London",
                Country = "United Kingdom",
                State = "UK",
                Street = "London street 236",
                ZipCode = "65365"
            },
            OrderItems = new List<OrderItemDto>
              {
                  new OrderItemDto
                  {
                       ProductId = Guid.NewGuid(),
                       ProductName = "Iphone 17",
                       Discount = 10,
                       ProductImageUrl = "iphone.jpg",
                       ProductSku = "626f96",
                       Quantity = 2,
                       UnitPrice = 1200
                  }
              }
        };


        var httpResponse = await WebFactory.HttpClient.PostAsJsonAsync("/orders", request);

        var response = await httpResponse.Content.ReadFromJsonAsync<OrderDetailsDto>();

        Assert.True(response is not null);
        Assert.Single(response.OrderItems);
        Assert.Equal(response.Status, OrderStatus.Draft.ToString());
    }
}
