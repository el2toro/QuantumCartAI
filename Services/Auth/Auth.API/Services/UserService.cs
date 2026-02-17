using Auth.API.Models;

namespace Auth.API.Services;

public interface IUserService
{
    User? ValidateUser(string username, string password);
    void Seed();
}
public class UserService : IUserService
{
    // In production → replace with a DB
    private readonly List<User> _users = new();

    public void Seed()
    {
        if (!_users.Any())
        {
            _users.Add(new User
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345"),
                Email = "test@test.com"
            });
        }
    }

    public User? ValidateUser(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username == username);
        if (user == null) return null;

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return user;
    }

    public bool EmailExists(string email)
    {
        return _users.Any(u => u.Email == email);
    }

    public void ResetPassword(string email, string newPassword)
    {
        var user = _users.FirstOrDefault(u => u.Email == email);
        if (user != null)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        }
    }
}
