using FA.Application.Dtos.BaseDtos;

namespace FA.Application.Dtos.BlogDtos;

public class BlogIndexDto : BaseEntityDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
