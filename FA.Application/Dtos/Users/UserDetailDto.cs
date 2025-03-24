using FA.Domain.Enumerations;

namespace FA.Application.Dtos.Users;

public class UserDetailDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    //public Guid CreatorId { get; set; }
    public UserPreviewDto Creator { get; set; } = null!;
    public DateTime UpdatedAt { get; set; }
    //public Guid UpdatorId { get; set; }
    public UserPreviewDto Updator { get; set; } = null!;

    public string Username { get; set; } = null!;
    public Role Role { get; set; }
    public string? Email { get; set; }
    public string? About { get; set; }
}
