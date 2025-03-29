using FA.Domain.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FA.Domain.Entities;

[Table(nameof(User))]
[Index(nameof(Username), IsUnique = true)]

public class User : BaseEntity
{
    [MaxLength(50)] public string Username { get; set; } = null!;
    [MaxLength(255)] public string Password { get; set; } = null!;
    public Role Role { get; set; }
    [EmailAddress][MaxLength(255)] public string? Email { get; set; }
    [MaxLength(1023)] public string? About { get; set; }

    public List<User> UsersICreated { get; set; } = [];
    public List<User> UsersIUpdated { get; set; } = [];

    public List<Blog> BlogsICreated { get; set; } = [];
    public List<Blog> BlogsIUpdated { get; set; } = [];

    public List<Category> CategoriesICreated { get; set; } = [];
    public List<Category> CategoriesIUpdated { get; set; } = [];

    public List<Post> PostsICreated { get; set; } = [];
    public List<Post> PostsIUpdated { get; set; } = [];

    public List<Tag> TagsICreated { get; set; } = [];
    public List<Tag> TagsIUpdated { get; set; } = [];

    public List<Comment> CommentsICreated { get; set; } = [];
    public List<Comment> CommentsIUpdated { get; set; } = [];

    public List<UploadFile> UploadFilesICreated { get; set; } = [];
    public List<UploadFile> UploadFilesIUpdated { get; set; } = [];
}
