using FA.Domain.Interfaces.Entities;

namespace FA.Application.Dtos.Posts;

public class RequestPostDto : IPost
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public double Rate { get; set; }
    public int Count { get; set; }
}
