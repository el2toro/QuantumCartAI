using Cart.Domain.ValueObjects;

namespace Cart.Application.Dtos;

public record CartDto(
    Guid CartId,
    List<CartItemDto> Items,
    Money Subtotal,
    Money? Discount,
    Money ShippingCost,
    Money Total,
    string? PromoCode,
    int SecondsUntilReserveExpiry   // for frontend countdown
);
