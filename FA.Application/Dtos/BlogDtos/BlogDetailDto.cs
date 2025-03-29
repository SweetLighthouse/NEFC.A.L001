using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.PostDtos;

namespace FA.Application.Dtos.BlogDtos;

public class BlogDetailDto : BaseEntityDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<PostIndexDto> Posts { get; set; } = [];
}
