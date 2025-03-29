using FA.Application.Dtos.BaseDtos;

namespace FA.Application.Dtos.TagDtos;

public class TagUpdateDto : LastUpdated
{
    public string? Name { get; set; }
}
