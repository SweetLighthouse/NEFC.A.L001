namespace FA.Application.Dtos.Permissions;

public class PermissionDto
{
    public string ModuleAction { get; set; } = string.Empty;
    public bool CanUser { get; set; }
    public bool CanContributor { get; set; }
    public bool CanBlogOwner { get; set; }
}

