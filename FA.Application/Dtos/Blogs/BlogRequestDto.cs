using System.ComponentModel.DataAnnotations;

namespace FA.Application.Dtos.Blogs;

public class BlogRequestDto
{
    [MaxLength(255)]
    public string Name { get; set; } = null!;
}
