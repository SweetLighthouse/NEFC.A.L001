using FA.Application.Dtos.BaseDtos;

namespace FA.Application.Dtos.CategoryDtos;

public class CategoryIndexDto : BaseEntityDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
