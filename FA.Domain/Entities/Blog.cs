using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.Domain.Entities;

[Table(nameof(Blog))]
public class Blog : BaseEntity
{
    [MaxLength(255)] public string Name { get; set; } = null!;
    [MaxLength(2014)] public string? Description { get; set; } = null!;
    public List<Post> Posts { get; set; } = [];
}