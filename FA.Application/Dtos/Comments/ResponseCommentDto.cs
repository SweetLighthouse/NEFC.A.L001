using FA.Domain.Interfaces;
using FA.Domain.Interfaces.Entities;

namespace FA.Application.Dtos.Comments;

public class ResponseCommentDto : IId, IMetadata, IComment
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public string Content { get; set; } = null!;
}
