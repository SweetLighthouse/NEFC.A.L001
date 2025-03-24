using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.Domain.Entities;

[Table(nameof(Comment))]
public class Comment : BaseEntity
{
    [MaxLength(1023)]
    public string Content { get; set; } = null!;

    // belong to 1 Post
    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
}
