using System.ComponentModel.DataAnnotations;

namespace FA.Application.Dtos.Accounts;

public class AccountUpdateDto
{
    [Length(3, 50, ErrorMessage = "Username must be between 3 and 50 characters long.")]
    public string? Username { get; set; }


    [Length(3, 50, ErrorMessage = "Password must be between 3 and 50 characters long.")]
    public string CurrentPassword { get; set; } = null!;


    [Length(3, 50, ErrorMessage = "Password must be between 3 and 50 characters long.")]
    public string? NewPassword { get; set; }


    [EmailAddress(ErrorMessage = "Email must be valid.")]
    public string? Email { get; set; }


    [MaxLength(1023)]
    public string? About { get; set; }
}
