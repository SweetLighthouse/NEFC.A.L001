using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FA.Application.Services;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class Permission(ModuleAction moduleAction) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Get the user's role from claims
        string? roleClaim = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        Enum.TryParse(roleClaim, out Role role);

        // unknown
        if (!permissionsTable.TryGetValue(moduleAction, out char[]? rolePermissions))
        {
            context.Result = new ForbidResult();
            return;
        }

        // check by Role
        if (rolePermissions[(int)role] != 'X')
        {
            context.Result = new ForbidResult();
            return;
        }
    }

    public static readonly Dictionary<ModuleAction, char[]> permissionsTable = new()
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
}