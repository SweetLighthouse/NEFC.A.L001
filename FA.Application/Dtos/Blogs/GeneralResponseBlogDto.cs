using FA.Domain.Entities;

namespace FA.Application.Dtos.Blogs;

public class GeneralResponseBlogDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
