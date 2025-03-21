using FA.Domain.Interfaces;
using FA.Domain.Interfaces.Entities;

namespace FA.Application.Dtos.Comments;

public class RequestCommentDto : IComment
{
    public string Content { get; set; }
}
