using FA.Domain.Interfaces.Entities;
using FA.Domain.Interfaces;

namespace FA.Domain.Entities;

public class Category : IId, IMetadata, ICategory, IIsDeleted
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
