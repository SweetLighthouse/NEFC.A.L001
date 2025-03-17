namespace FA.Domain.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public double Rate { get; set; }
    public int Count { get; set; }

    public Guid BlogId { get; set; }
    public Blog Blog { get; set; } = null!;
    public List<Category> Categories { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
}