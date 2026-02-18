using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Exceptions;

public class DomainException : Exception
{
    public virtual int StatusCode => StatusCodes.Status409Conflict;
    public virtual string Title => "Business Rule Violation";

    protected DomainException(string message) : base(message) { }
}
