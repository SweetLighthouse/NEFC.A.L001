using FA.Domain.Interfaces;
using FA.Domain.Interfaces.Entities;

namespace FA.Application.Dtos.Posts;

public class ResponsePostDto : IId, IMetadata, IPost
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public double Rate { get; set; }
    public int Count { get; set; }
}
