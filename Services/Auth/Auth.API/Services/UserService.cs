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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345")
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
}
