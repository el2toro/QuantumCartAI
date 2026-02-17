namespace Cart.Application.Dtos;

public record CartItemDto(Guid ProductId, int Quantity, decimal Price, decimal DiscountedPrice);


