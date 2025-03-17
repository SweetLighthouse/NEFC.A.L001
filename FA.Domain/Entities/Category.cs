namespace FA.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;

    public List<Post> Posts { get; set; } = [];
}
