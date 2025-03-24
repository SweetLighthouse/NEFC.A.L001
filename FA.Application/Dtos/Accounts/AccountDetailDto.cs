using FA.Domain.Enumerations;

namespace FA.Application.Dtos.Accounts;

public class AccountDetailDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Username { get; set; } = null!;
    public Role Role { get; set; }
    public string? Email { get; set; }
    public string? About { get; set; }
}
