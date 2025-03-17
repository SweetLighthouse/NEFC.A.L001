using FA.Domain.Enumerations;

namespace FA.Application.Dtos.Users;

public class RequestUserDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public List<Role> Roles { get; set; } = [];

    public string? About { get; set; }
    public string? Email { get; set; }
}
