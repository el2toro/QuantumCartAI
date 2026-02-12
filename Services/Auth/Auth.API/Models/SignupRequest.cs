namespace Auth.API.Models;

public record SignupRequest(string Name, string Email, string Password, bool Terms);

