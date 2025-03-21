using FA.Domain.Enumerations;
using System.Text.RegularExpressions;

namespace FA.Application.Services;

public class AuthorizerService
{
    private readonly Dictionary<ModuleAction, char[]> permissions = new()
    {
        { ModuleAction.CreatePost,      new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.UpdatePost,      new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.DetailsPost,     new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.IndexPost,       new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.DeletePost,      new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.PublishPost,     new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.UnpublishPost,   new char[] { ' ' , ' ' , 'X'  } },

        { ModuleAction.CreateBlog,      new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.UpdateBlog,      new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.DetailsBlog,     new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.IndexBlog,       new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.DeleteBlog,      new char[] { ' ' , ' ' , 'X'  } },

        { ModuleAction.CreateCategory,  new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.UpdateCategory,  new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.DetailsCategory, new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.IndexCategory,   new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.DeleteCategory,  new char[] { ' ' , ' ' , 'X'  } },

        { ModuleAction.CreateTag,       new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.UpdateTag,       new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.DetailsTag,      new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.IndexTag,        new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.DeleteTag,       new char[] { ' ' , ' ' , 'X'  } },

        { ModuleAction.CreateComment,   new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.UpdateComment,   new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.DetailsComment,  new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.IndexComment,    new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.DeleteComment,   new char[] { ' ' , ' ' , 'X'  } },

        { ModuleAction.CreateRole,      new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.UpdateRole,      new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.DetailsRole,     new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.IndexRole,       new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.DeleteRole,      new char[] { ' ' , ' ' , 'X'  } },

        { ModuleAction.CreateUser,      new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.UpdateUser,      new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.DetailsUser,     new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.IndexUser,       new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.DeleteUser,      new char[] { ' ' , ' ' , 'X'  } },
    };

    public Dictionary<ModuleAction, char[]> GetPermissionsTable()
    {
        return permissions;
    }


    public bool HasPermission(Role role, ModuleAction moduleAction)
    {
        char[]? rolePermissions;

        // action not registered.
        if (!permissions.TryGetValue(moduleAction, out rolePermissions)) return false;

        // check by Role
        return rolePermissions[(int)role] == 'X';
    }

    public bool HasPermission(string? roleRaw, ModuleAction moduleAction)
    {
        Enum.TryParse<Role>(roleRaw, out var role);
        return HasPermission(role, moduleAction);
    }

    public bool IsValidUsername(string username)
    {
        return username.Length >= 6 && !username.Contains(' ');
    }

    public bool IsValidPassword(string password)
    {
        // At least 6 characters, no spaces, only letters, numbers, underscores
        return Regex.IsMatch(password, @"^[a-zA-Z0-9_]{6,}$");
    }
}