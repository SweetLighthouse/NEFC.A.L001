namespace FA.Domain.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; } = null!;
    public int Like { get; set; }
    public int Dislike { get; set; }

    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
}
