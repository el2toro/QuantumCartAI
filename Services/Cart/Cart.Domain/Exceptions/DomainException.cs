namespace Cart.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string msg) : base(msg) { }
}
