using FA.Application.Dtos.BaseDtos;
using FA.Domain.Enumerations;

namespace FA.Application.Dtos.Users;

public class UserIndexDto : BaseEntityDto
{
    public string Username { get; set; } = null!;
    public Role Role { get; set; }
    public string Email { get; set; } = null!;
    public string About { get; set; } = null!;
}
