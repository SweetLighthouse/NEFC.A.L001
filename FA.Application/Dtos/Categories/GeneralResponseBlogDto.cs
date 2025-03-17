using FA.Domain.Entities;

namespace FA.Application.Dtos.Categories;

public class GeneralResponseCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
