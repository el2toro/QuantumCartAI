namespace Cart.Application.Dtos;

public class CartItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountedPrice { get; set; }
};


