using System.ComponentModel.DataAnnotations;

namespace FA.Domain.Entities;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid CreatorId { get; set; }
    public User Creator { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
    public Guid UpdatorId { get; set; }
    public User Updator { get; set; } = null!;
    public bool IsDeleted { get; set; }
}
