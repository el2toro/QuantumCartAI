namespace Cart.Domain.Exceptions;

public class ItemNotFoundException : DomainException
{
    public ItemNotFoundException() : base("Item not in cart") { }
}
