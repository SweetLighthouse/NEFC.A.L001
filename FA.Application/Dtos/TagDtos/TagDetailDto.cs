using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.PostDtos;

namespace FA.Application.Dtos.TagDtos;

public class TagDetailDto : BaseEntityDto
{
    public string Name { get; set; } = null!;
    public List<PostIndexDto> Posts { get; set; } = [];
}
