namespace Cart.Application.Dtos;

public record ProductQueryDto(Guid ProductId, int Quantity, decimal Price);

