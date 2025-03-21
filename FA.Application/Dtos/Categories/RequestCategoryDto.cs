using FA.Domain.Interfaces.Entities;

namespace FA.Application.Dtos.Categories;

public class RequestCategoryDto : ICategory
{
    public string Name { get; set; } = null!;
}
