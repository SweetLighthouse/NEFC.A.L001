using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.Domain.Entities;

[Table(nameof(Category))]
public class Category : BaseEntity
{
    [MaxLength(255)] public string Name { get; set; } = null!;
    [MaxLength(1024)] public string? Description { get; set; } = null!;
    public List<Post> Posts { get; set; } = [];
    public List<PostCategory> PostCategories { get; set; } = [];
}
