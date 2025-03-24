namespace FA.Application.Dtos.Users;

public class UserPreviewDto
{
    public Guid Id { get; set; }
    //public DateTime CreatedAt { get; set; }
    //public Guid CreatedById { get; set; }
    //public DateTime UpdatedAt { get; set; }
    //public Guid UpdatedById { get; set; }
    //public bool IsDeleted { get; set; }

    public string Username { get; set; } = null!;
    //public Role Role { get; set; }
    //public string? Email { get; set; }
    //public string? About { get; set; }
}
