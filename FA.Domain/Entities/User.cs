﻿using FA.Domain.Enumerations;
using FA.Domain.Interfaces;
using FA.Domain.Interfaces.Entities;

namespace FA.Domain.Entities;

public class User : IId, IMetadata, IUser, IIsDeleted
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }

    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Role Role { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? About { get; set; }
    public bool IsDeleted { get; set; }
}
