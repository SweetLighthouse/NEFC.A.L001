using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.Domain.Entities;

[Table(nameof(Tag))]
public class Tag : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    // Many to Many Post
    public List<Post> Posts { get; set; } = [];
    public List<PostTag> PostTags { get; set; } = [];
}

