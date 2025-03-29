namespace FA.Application.Dtos.BaseDtos;

public class PageResultViewDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItem { get; set; }
    public int TotalPage => (int)Math.Ceiling((float)TotalItem / PageSize);
    public string? ControllerName { get; set; }
}
