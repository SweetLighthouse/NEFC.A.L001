using System.ComponentModel.DataAnnotations;

namespace FA.Application.Dtos.AccountDtos;

public class RegisterDto
{
    [Required(ErrorMessage = "Username is required, bro.")]
    [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters long.")]
    public string Username { get; set; } = null!;


    [Required(ErrorMessage = "Password is required, bro.")]
    [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Password must be between 3 and 50 characters long.")]
    public string Password { get; set; } = null!;


    [EmailAddress(ErrorMessage = "Email must be valid.")]
    public string? Email { get; set; }
    
    
    public string? About { get; set; }
}