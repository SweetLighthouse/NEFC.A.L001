using FA.Application.Dtos.BaseDtos;
using System.ComponentModel.DataAnnotations;

namespace FA.Application.Dtos.BlogDtos;

public class BlogUpdateDto : LastUpdated
{
    [MaxLength(255)] public string? Name { get; set; } = null!;
    [MaxLength(2014)] public string? Description { get; set; } = null!;
}
