using FA.Domain.Interfaces.Entities;

namespace FA.Application.Dtos.Blogs;

public class RequestBlogDto : IBlog
{
    public string Name { get; set; } = null!;
}
