using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.PostDtos;
using System.ComponentModel.DataAnnotations;

namespace FA.Application.Dtos.CategoryDtos;

public class CategoryDetailDto : BaseEntityDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<PostIndexDto> Posts { get; set; } = [];
}
