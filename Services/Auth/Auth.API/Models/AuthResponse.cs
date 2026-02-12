using Auth.API.DTOs;

namespace Auth.API.Models;

public record AuthResponse(string AccessToken, string RefreshToken, UserDto User);
