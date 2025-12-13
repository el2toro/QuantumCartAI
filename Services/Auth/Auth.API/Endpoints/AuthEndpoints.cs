using Auth.API.DTOs;
using Auth.API.Models;
using Auth.API.Services;
using Carter;

namespace Auth.API.Endpoints;

public class AuthEndpoints : ICarterModule
{
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

        });

        app.MapPost("auth/reset-password", (string email) =>
        {

        });
    }
}
