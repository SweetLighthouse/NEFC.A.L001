namespace FA.Domain.Entities;

public class Blog : BaseEntity
{
    public string Name { get; set; } = null!;

    public List<Post> Posts { get; set; } = [];
}