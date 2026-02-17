using System.Security.Claims;

namespace QuantumCartAI.Shared.Infrastructure.AspNetCore.Extensions;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Extracts the authenticated user's ID from claims.
    /// Uses the standard ClaimTypes.NameIdentifier (sub / nameidentifier).
    /// Returns null if not found or invalid format.
    /// </summary>
    /// <param name="principal">The current ClaimsPrincipal (HttpContext.User)</param>
    /// <returns>Guid of the user or null if missing/invalid</returns>
    public static Guid? GetUserId(this ClaimsPrincipal? principal)
    {
        if (principal?.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)
                        ?? principal.FindFirst("sub"); // fallback for raw JWT without mapping

        if (userIdClaim == null || string.IsNullOrWhiteSpace(userIdClaim.Value))
        {
            return null;
        }

        // Most common: GUID as string
        if (Guid.TryParse(userIdClaim.Value, out var guid))
        {
            return guid;
        }

        // Optional: support other formats (e.g. int/long/string ids) if needed in future
        // For strict Guid-only systems you can throw here instead:
        // throw new InvalidOperationException($"User ID claim value is not a valid GUID: {userIdClaim.Value}");

        return null;
    }

    // Optional: variant that throws (useful in commands/handlers where auth is required)
    public static Guid GetRequiredUserId(this ClaimsPrincipal principal)
    {
        var id = principal.GetUserId();
        if (!id.HasValue)
        {
            throw new InvalidOperationException("Authenticated user ID not found in claims.");
        }
        return id.Value;
    }
}
