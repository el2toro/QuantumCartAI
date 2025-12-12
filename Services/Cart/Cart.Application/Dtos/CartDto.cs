namespace Cart.Application.Dtos;

public class CartDto
{
    public Guid Id { get; set; }
    public Guid? CustomerId { get; set; }
    public IEnumerable<CartItemDto> CartItems { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
}

