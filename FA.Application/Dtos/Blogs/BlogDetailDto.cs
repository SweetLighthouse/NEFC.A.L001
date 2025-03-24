using FA.Application.Dtos.Users;

namespace FA.Application.Dtos.Blogs;

public class BlogDetailDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserPreviewDto Creator { get; set; } = null!;
    public string Name { get; set; } = null!;
}
