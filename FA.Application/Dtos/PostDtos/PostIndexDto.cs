using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.BlogDtos;
using FA.Application.Dtos.CategoryDtos;
using FA.Application.Dtos.TagDtos;

namespace FA.Application.Dtos.PostDtos;

public class PostIndexDto : BaseEntityDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int ViewCount { get; set; }

    public BlogIndexDto Blog { get; set; } = null!;
    public List<CategoryIndexDto> Categories { get; set; } = [];
    public List<TagIndexDto> Tags { get; set; } = [];
}
