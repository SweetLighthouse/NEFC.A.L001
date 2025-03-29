using FA.Application.Dtos.Users;

namespace FA.Application.Dtos.BaseDtos;

public class BaseEntityDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserPreviewDto Creator { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
    public UserPreviewDto Updator { get; set; } = null!;
    public bool IsDeleted { get; set; }
}
