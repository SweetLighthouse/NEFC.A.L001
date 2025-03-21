using FA.Domain.Enumerations;

namespace FA.Domain.Interfaces.Entities;

public interface IUser
{
    string Username { get; set; } 
    string Password { get; set; }
    Role Role { get; set; }
    string? Name { get; set; }
    string? Email { get; set; }
    string? About { get; set; }
}
