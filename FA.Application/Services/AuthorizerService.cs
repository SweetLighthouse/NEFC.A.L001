using FA.Domain.Enumerations;

namespace FA.Application.Services;

public class AuthorizerService
{
    private readonly Dictionary<ModuleAction, char[]> permissions = new()
    {
        { ModuleAction.Post.Create,      new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.Post.Update,      new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.Post.Details,     new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.Post.Index,       new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.Post.Delete,      new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.Post.Publish,     new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.Post.Unpublish,   new char[] { ' ' , ' ' , 'X'  } },

        { ModuleAction.Blog.Create,      new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.Blog.Update,      new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.Blog.Details,     new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.Blog.Index,       new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.Blog.Delete,      new char[] { ' ' , ' ' , 'X'  } },

        { ModuleAction.Category.Create,  new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.Category.Update,  new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.Category.Details, new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.Category.Index,   new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.Category.Delete,  new char[] { ' ' , ' ' , 'X'  } },

        { ModuleAction.Tag.Create,       new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.Tag.Update,       new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.Tag.Details,      new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.Tag.Index,        new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.Tag.Delete,       new char[] { ' ' , ' ' , 'X'  } },

        { ModuleAction.Comment.Create,   new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.Comment.Update,   new char[] { ' ' , 'X' , 'X'  } },
        { ModuleAction.Comment.Details,  new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.Comment.Index,    new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.Comment.Delete,   new char[] { ' ' , ' ' , 'X'  } },

        { ModuleAction.Role.Create,      new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.Role.Update,      new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.Role.Details,     new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.Role.Index,       new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.Role.Delete,      new char[] { ' ' , ' ' , 'X'  } },

        { ModuleAction.User.Create,      new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.User.Update,      new char[] { ' ' , ' ' , 'X'  } },
        { ModuleAction.User.Details,     new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.User.Index,       new char[] { 'X' , 'X' , 'X'  } },
        { ModuleAction.User.Delete,      new char[] { ' ' , ' ' , 'X'  } },
    };

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
}