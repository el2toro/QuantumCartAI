using Microsoft.AspNetCore.Http;

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
        var anonId = context.Request.Cookies[AnonymousCookieName];

        // 1. If cookie exists and is valid GUID → attach to HttpContext
        if (!string.IsNullOrWhiteSpace(anonId) && Guid.TryParse(anonId, out var guid))
        {
            context.Items["AnonymousUserId"] = guid;
        }
        else
        {
            // 2. No cookie or invalid → create new anonymous session
            guid = Guid.NewGuid();

            context.Items["AnonymousUserId"] = guid;

            // Set HttpOnly, Secure, SameSite=Strict cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,                              // forces HTTPS
                SameSite = SameSiteMode.Strict,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddDays(CookieDays),
                IsEssential = true                          // GDPR: still works when user rejects non-essential cookies
            };

            context.Response.Cookies.Append(AnonymousCookieName, guid.ToString(), cookieOptions);
        }

        // Optional: sliding expiration – refresh TTL in Redis on every request
        // We do this in CartService/AIRecommendationService, not here

        await _next(context);
    }
}
