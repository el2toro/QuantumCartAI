using Microsoft.AspNetCore.Http;

namespace QuantumCartAI.Shared.Infrastructure.AspNetCore.Session;

public class CurrentSession
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentSession(IHttpContextAccessor accessor)
    {
        _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
    }

    public Guid AnonymousId
    {
        get
        {
            var id = _accessor.HttpContext?.Items["AnonymousUserId"] as Guid?;
            return id ?? throw new InvalidOperationException("No anonymous session found in current context");
        }
    }

    public Guid? AuthenticatedUserId
    {
        get => _accessor.HttpContext?.User?.FindFirst("sub")?.Value is string sub ? Guid.Parse(sub) : null;
        // or use ClaimTypes.NameIdentifier depending on your token structure
    }

    public bool IsAnonymous => AuthenticatedUserId == null;

    public bool IsAuthenticated => !IsAnonymous;

    // Optional: convenience properties
    public bool HasAnonymousSession => _accessor.HttpContext?.Items.ContainsKey("AnonymousUserId") == true;
}
