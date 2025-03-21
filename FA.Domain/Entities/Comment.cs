using FA.Domain.Interfaces;
using FA.Domain.Interfaces.Entities;

namespace FA.Domain.Entities;

public class Comment : IId, IMetadata, IComment, IIsDeleted
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }

    public string Content { get; set; } = null!;
    public bool IsDeleted { get; set; }

    //public Guid PostId { get; set; }
    //public Post Post { get; set; } = null!;
}
