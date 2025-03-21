namespace FA.Application.Dtos.Accounts;

public class RegisterDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? About { get; set; }
}
