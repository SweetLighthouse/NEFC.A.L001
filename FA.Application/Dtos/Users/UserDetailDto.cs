using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.PostDtos;
using FA.Domain.Enumerations;

namespace FA.Application.Dtos.Users;

public class UserDetailDto : BaseEntityDto
{
    public string Username { get; set; } = null!;
    public Role Role { get; set; }
    public string Email { get; set; } = null!;
    public string About { get; set; } = null!;
    public List<PostIndexDto> PostsICreated { get; set; } = [];
}
