using FA.Domain.Interfaces;
using FA.Domain.Interfaces.Entities;

namespace FA.Application.Dtos.Tags;

public class RequestTagDto : ITag
{
    public string Name { get; set; } = null!;
}
