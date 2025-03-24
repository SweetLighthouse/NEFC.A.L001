﻿using FA.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace FA.Application.Dtos.Users;

public class UserUpdateDto
{
    [Length(3, 50, ErrorMessage = "Username must be between 3 and 50 characters long.")]
    public string Username { get; set; } = null!;



    [Required(ErrorMessage = "Role is required.")]
    public Role? Role { get; set; }



    [EmailAddress(ErrorMessage = "Email must be valid.")]
    public string? Email { get; set; }


    public string? About { get; set; }
}
