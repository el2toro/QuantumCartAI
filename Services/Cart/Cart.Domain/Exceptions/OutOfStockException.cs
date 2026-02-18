using BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Cart.Domain.Exceptions;

public class OutOfStockException(string message) : DomainException(message)
{
    public override string Title => "Product Out of Stock";
    public override int StatusCode => StatusCodes.Status409Conflict;
}