using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.Domain.Entities;

[Table(nameof(Blog))]
public class Blog : BaseEntity
{
    [MaxLength(255)]
    public string Name { get; set; } = null!;
    public List<Post> Posts { get; set; } = [];
}