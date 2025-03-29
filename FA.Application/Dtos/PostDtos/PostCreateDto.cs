using System.ComponentModel.DataAnnotations;

namespace FA.Application.Dtos.PostDtos;

public class PostCreateDto
{
    [MaxLength(255)] public string Title { get; set; } = null!;
    [MaxLength(8192)] public string Content { get; set; } = null!;
    public Guid BlogId { get; set; }
    public List<Guid> CategoryIds { get; set; } = [];
    public List<string> TagNames { get; set; } = [];
}
