using FA.Domain.Interfaces;
using FA.Domain.Interfaces.Entities;

namespace FA.Domain.Entities;

public class Tag : IId, IMetadata, ITag, IIsDeleted
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }

    public string Name { get; set; } = null!;
    public bool IsDeleted { get; set; }
    //public List<Post> Posts { get; set; } = [];
}

