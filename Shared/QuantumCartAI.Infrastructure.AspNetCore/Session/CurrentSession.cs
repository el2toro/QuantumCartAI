using Microsoft.AspNetCore.Http;
using System.Security.Claims;

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
            string? anonymousId = _accessor.HttpContext?.Request?.Headers?["X-Anonymous-Id"]
                                  ?? throw new InvalidOperationException("No anonymous session found in current context");

            Guid.TryParse(anonymousId, out Guid id);

            return id;
        }
    }

    public Guid? AuthenticatedUserId
    {

        get
        {
            string? userId = _accessor.HttpContext?.User?.Identity?.IsAuthenticated == true
                             ? _accessor.HttpContext?.User.Claims.FirstOrDefault()?.Value
                             : null;

            return userId is not null
                ? Guid.Parse(userId)
                : null;
        }
    }

    public bool IsAnonymous => AuthenticatedUserId == null;

    public bool IsAuthenticated => !IsAnonymous;
}
