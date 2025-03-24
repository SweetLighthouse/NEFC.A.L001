using System.ComponentModel.DataAnnotations;

namespace FA.Application.Dtos.Users;

public class UserChangePasswordDto
{
    //[Length(3, 50, ErrorMessage = "Password must be between 3 and 50 characters long.")]
    //public string OldPassword { get; set; } = null!;


    [Length(3, 50, ErrorMessage = "Password must be between 3 and 50 characters long.")]
    public string NewPassword { get; set; } = null!;
}
