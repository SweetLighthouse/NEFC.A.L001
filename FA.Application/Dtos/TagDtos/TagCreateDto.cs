using System.ComponentModel.DataAnnotations;

namespace FA.Application.Dtos.TagDtos;

public class TagCreateDto
{
    [MaxLength(255)]
    public string Name { get; set; } = null!;
}
