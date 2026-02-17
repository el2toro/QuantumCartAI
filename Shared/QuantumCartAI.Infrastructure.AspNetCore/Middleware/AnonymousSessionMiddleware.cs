using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using QuantumCartAI.Shared.Infrastructure.AspNetCore.Extensions;

namespace QuantumCartAI.Shared.Infrastructure.AspNetCore.Middleware;

public class AnonymousSessionMiddleware
{
    private readonly RequestDelegate _next;
    private const string AnonymousCookieName = "X-AnonSessionId";
    private const int CookieDays = 90;

    public AnonymousSessionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var anonIdStr = context.Request.Cookies[AnonymousCookieName];

        Guid? anonId = null;

        if (!string.IsNullOrWhiteSpace(anonIdStr) && Guid.TryParse(anonIdStr, out var parsed))
        {
            anonId = parsed;
        }

        // ── New logic ──
        var isAuthenticated = context.User?.Identity?.IsAuthenticated == true;

        if (isAuthenticated)
        {
            var userId = context.User.GetUserId();
            if (userId.HasValue)
            {
                context.Items["CurrentPrincipalId"] = userId.Value;
                context.Items["IsAnonymous"] = false;
                // Optional: context.Items["UserId"] = userId.Value;  // legacy name if needed
            }
            else
            {
                // Rare – token valid but no sub claim → log & treat as anon or reject
                context.Items["IsAnonymous"] = true;
            }
        }
        else if (anonId.HasValue)
        {
            context.Items["CurrentPrincipalId"] = anonId.Value;
            context.Items["IsAnonymous"] = true;
        }
        else
        {
            // Guest + no cookie → create
            anonId = Guid.NewGuid();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // helper extension
                SameSite = SameSiteMode.Lax,                 // ← usually better than Strict
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddDays(CookieDays), // longer is better
                IsEssential = true
            };

            context.Response.Cookies.Append(AnonymousCookieName, anonId.Value.ToString(), cookieOptions);

            context.Items["CurrentPrincipalId"] = anonId.Value;
            context.Items["IsAnonymous"] = true;
        }

        await _next(context);
    }
}
