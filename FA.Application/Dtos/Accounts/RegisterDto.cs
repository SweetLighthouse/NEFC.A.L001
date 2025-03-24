using System.ComponentModel.DataAnnotations;

namespace FA.Application.Dtos.Accounts;

public class RegisterDto
{
    [Length(3, 50, ErrorMessage = "Username must be between 3 and 50 characters long.")]
    public string Username { get; set; } = null!;


    [Length(3, 50, ErrorMessage = "Password must be between 3 and 50 characters long.")]
    public string Password { get; set; } = null!;


    [EmailAddress(ErrorMessage = "Email must be valid.")]
    public string? Email { get; set; }
    
    
    public string? About { get; set; }
}