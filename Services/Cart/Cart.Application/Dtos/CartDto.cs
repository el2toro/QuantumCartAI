namespace Cart.Application.Dtos;

public record CartDto(Guid Id, Guid? CustomerId, IEnumerable<CartItemDto> CartItems, decimal Subtotal);

