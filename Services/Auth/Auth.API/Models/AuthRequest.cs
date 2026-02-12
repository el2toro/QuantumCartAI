namespace Auth.API.Models;

public record AuthRequest(string Email, string Password, bool RememberMe);