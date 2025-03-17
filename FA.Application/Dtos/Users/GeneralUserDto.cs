using FA.Domain.Enumerations;

namespace FA.Application.Dtos.Users;

public class GeneralUserDto
{
    public string Username { get; set; } = null!;
    public List<Role> Roles { get; set; } = [];

    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? About { get; set; }
}
