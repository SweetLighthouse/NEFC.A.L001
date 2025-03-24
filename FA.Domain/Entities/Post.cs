using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.Domain.Entities;

[Table(nameof(Post))]
public class Post : BaseEntity
{
    [MaxLength(255)]
    public string Title { get; set; } = null!;

    [MaxLength(8192)]
    public string Content { get; set; } = null!;

    [Range(0, 10)]
    public double Rate { get; set; }

    [Range(0, int.MaxValue)]
    public int ViewCount { get; set; }

    // One Blog
    public Guid BlogId { get; set; }
    public Blog Blog { get; set; } = null!;

    // One Category
    public Guid CategoryId { get; set; } 
    public Category Category { get; set; } = null!;

    // Many to Many Tag
    public List<Tag> Tags { get; set; } = [];
    public List<PostTag> PostTags { get; set; } = [];

    // Has many Comment
    public List<Comment> Comments { get; set; } = [];
}