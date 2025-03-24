namespace FA.Application.Dtos.Users;

public class UserChangePasswordViewDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
