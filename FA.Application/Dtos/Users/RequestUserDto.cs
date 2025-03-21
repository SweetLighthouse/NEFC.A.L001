using FA.Domain.Enumerations;
using FA.Domain.Interfaces.Entities;

namespace FA.Application.Dtos.Users;

public class RequestUserDto : IUser
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Role Role { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? About { get; set; }
}
