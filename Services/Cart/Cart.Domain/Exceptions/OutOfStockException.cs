namespace Cart.Domain.Exceptions;

public class OutOfStockException : DomainException
{
    public OutOfStockException(string msg) : base(msg) { }
}