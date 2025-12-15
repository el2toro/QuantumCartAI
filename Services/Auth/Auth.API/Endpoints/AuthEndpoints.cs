using Auth.API.DTOs;
using Auth.API.Models;
using Auth.API.Services;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Endpoints;

public class AuthEndpoints : ICarterModule
{
    public record ConfirmEmailRequest(string Email);
    public record ResetPasswordRequest(string Email);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", (AuthRequest req,
                            IUserService users,
                            ITokenService tokens) =>
        {
            var user = users.ValidateUser(req.Email, req.Password);
            if (user is null)
                return Results.Unauthorized();

            var access = tokens.GenerateAccessToken(user);
            var refresh = tokens.GenerateRefreshToken();

            var userDto = new UserDto(Guid.NewGuid(), user.Username, user.Username, "admin@dev.com");

            return Results.Ok(new AuthResponse(access, refresh, userDto));
        });

        app.MapPost("auth/signup", (SignupRequest request) =>
        {
            return Results.Ok();
        });

        app.MapPost("auth/confirm-email", (ConfirmEmailRequest request) =>
        {
            return Results.Ok();
        });

        app.MapPost("auth/reset-password", (ResetPasswordRequest request) =>
        {
            return Results.Ok();
        });
    }
}
