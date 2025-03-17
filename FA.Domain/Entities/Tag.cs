namespace FA.Domain.Entities;

public class Tag : BaseEntity
{
    public string? Name { get; set; }

    // has many Posts
    public List<Post> Posts { get; set; } = [];
}
