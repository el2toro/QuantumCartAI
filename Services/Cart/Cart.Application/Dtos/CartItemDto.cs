using Cart.Domain.ValueObjects;

namespace Cart.Application.Dtos;

public record CartItemDto(
    string SkuId,
    int Quantity,
    Money UnitPrice,
    Money LineTotal
);
