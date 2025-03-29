using System.ComponentModel.DataAnnotations;

namespace FA.Application.Dtos.CategoryDtos;

public class CategoryCreateDto
{
    [MaxLength(255)] public string Name { get; set; } = null!;
    [MaxLength(1024)] public string? Description { get; set; } = null!;
}
