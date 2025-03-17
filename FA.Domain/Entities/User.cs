using FA.Domain.Enumerations;

namespace FA.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public List<Role> Roles { get; set; } = [];

    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? About { get; set; }
}
