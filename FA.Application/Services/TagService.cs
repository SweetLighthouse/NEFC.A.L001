using AutoMapper;
using FA.Domain.Entities;
using FA.Infrastructure.Context;
using FA.Application.Dtos.Tags;

namespace FA.Application.Services;

public class TagService : GenericService<Tag, RequestTagDto, ResponseTagDto>
{
    public TagService(MainDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
