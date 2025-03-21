using AutoMapper;
using FA.Domain.Entities;
using FA.Application.Dtos.Blogs;
using FA.Infrastructure.Context;

namespace FA.Application.Services;

public class BlogService : GenericService<Blog, RequestBlogDto, ResponseBlogDto>
{
    public BlogService(MainDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
