using FA.Application.Dtos.BaseDtos;
using System.ComponentModel.DataAnnotations;

namespace FA.Application.Dtos.CategoryDtos;

public class CategoryUpdateDto : LastUpdated
{
    [MaxLength(255)] public string? Name { get; set; } = null!;
    [MaxLength(1024)] public string? Description { get; set; } = null!;
}
