using FA.Application.Dtos.BaseDtos;
using FA.Domain.Enumerations;

namespace FA.Application.Dtos.AccountDtos;

public class AccountDetailDto : BaseEntityDto
{
    public string Username { get; set; } = null!;
    public Role Role { get; set; }
    public string? Email { get; set; }
    public string? About { get; set; }
}
