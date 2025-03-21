using FA.Domain.Interfaces;
using FA.Domain.Interfaces.Entities;

namespace FA.Domain.Entities;

public class Post : IId, IMetadata, IPost, IIsDeleted
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
    public bool IsDeleted { get; set; }

    //public Guid BlogId { get; set; } 
    //public Blog Blog { get; set; } = null!;
    //public Guid CategoryId { get; set; } 
    //public Category Category { get; set; } = null!;
    //public List<Comment> Comments { get; set; } = [];
    //public List<Tag> Tags { get; set; } = []; 
}