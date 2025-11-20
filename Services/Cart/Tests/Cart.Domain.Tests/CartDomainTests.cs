using Cart.Domain.ValueObjects;

namespace Cart.Domain.Tests
{
    public class CartDomainTests
    {
        [Theory]
        [InlineData(2, 50, Currency.USD, 100, Currency.USD, 1, 2, 0)]
        [InlineData(3, 20, Currency.EUR, 60, Currency.EUR, 1, 3, 0)]
        public void AddItem_Should_Add_Item_To_Cart_With_Correct_Currency(
            int productQuantity,
            int price,
            Currency currency,
            int expectedSubtotal,
            Currency expectedCurrency,
            int expectedItemsCount,
            int expectedProductQuantity,
            int itemCount)
        {
            var cart = new Entities.Cart(CartId.New(), currency);

            cart.AddItem(ProductId.New(), Quantity.From(productQuantity), new Money(price, currency));
            // cart.ShippingCost.

            Assert.NotEmpty(cart.Items);
            Assert.True(cart.Subtotal == new Money(expectedSubtotal, Currency: expectedCurrency));
            Assert.True(cart.Items.Count == expectedItemsCount);
            Assert.True(cart.Items[itemCount].Quantity == Quantity.From(expectedProductQuantity));
        }
    }
}