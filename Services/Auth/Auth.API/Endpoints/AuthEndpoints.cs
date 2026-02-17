using Auth.API.DTOs;
using Auth.API.Models;
using Auth.API.Services;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Endpoints;

public class AuthEndpoints : ICarterModule
{
    public record ConfirmEmailRequest(string Email);
    public record ResetPasswordRequest(string Email, string NewPassword, string ConfirmPassword);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", (AuthRequest req, ITokenService tokenService) =>
        {
            var user = new User { Id = Guid.NewGuid(), Email = "admin@dev.com", Username = "admin", PasswordHash = "10" }; //users.ValidateUser(req.Email, req.Password);
            if (user is null)
                return Results.Unauthorized();

            var access = tokenService.GenerateAccessToken(user);
            var refresh = tokenService.GenerateRefreshToken();

            var userDto = new UserDto(Guid.NewGuid(), user.Username, user.Username, user.Email);

            return Results.Ok(new AuthResponse(access, refresh, userDto));
        });

        app.MapPost("auth/signup", (SignupRequest request) =>
        {
            return Results.Ok();
        });

        app.MapPost("auth/confirm-email", (ConfirmEmailRequest request) =>
        {
            //if (!userService.EmailExists(request.Email))
            //   return Results.NotFound();

            return Results.Ok();
        });

        app.MapPost("auth/reset-password", (ResetPasswordRequest request) =>
        {
            if (request.NewPassword != request.ConfirmPassword)
                return Results.BadRequest("Passwords do not match.");

            // userService.ResetPassword(request.Email, request.NewPassword);

            return Results.Ok();
        });
    }
}
