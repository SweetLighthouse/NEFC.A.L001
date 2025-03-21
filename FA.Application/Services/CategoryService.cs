using AutoMapper;
using FA.Domain.Entities;
using FA.Application.Dtos.Categories;
using FA.Infrastructure.Context;

namespace FA.Application.Services;  

public class CategoryService : GenericService<Category, RequestCategoryDto, ResponseCategoryDto>
{
    public CategoryService(MainDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
