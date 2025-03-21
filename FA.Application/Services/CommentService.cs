using AutoMapper;
using FA.Application.Dtos.Comments;
using FA.Domain.Entities;
using FA.Infrastructure.Context;

namespace FA.Application.Services;

public class CommentService : GenericService<Comment, RequestCommentDto, ResponseCommentDto>
{
    public CommentService(MainDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
