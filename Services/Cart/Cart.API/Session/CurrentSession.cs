namespace Cart.API.Session;

public class CurrentSession
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentSession(IHttpContextAccessor accessor) => _accessor = accessor;

    public Guid Id => _accessor.HttpContext?.Items["AnonymousUserId"] as Guid?
                     ?? throw new InvalidOperationException("No session");

    public bool IsAnonymous => _accessor.HttpContext?.User?.Identity?.IsAuthenticated != true;
}