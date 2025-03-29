using FA.Application.Dtos.Permissions;
using FA.Domain.Enumerations;
using System.Security.Claims;

namespace WebApp.Services;

public class AuthorizerService
{
    private readonly Role? _role;
    
    public  AuthorizerService(IHttpContextAccessor httpContextAccessor)
    {
        if (Enum.TryParse(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value, out Role role))
        {
            _role = role;
        }
    }

    public bool HasPermission(ModuleAction moduleAction)
    {
        PermissionDto? permission = PermissionTable.Permissions.FirstOrDefault(p => p.ModuleAction == moduleAction.ToString());
        if (permission == null) return false;
        return _role switch
        {
            Role.User => permission.CanUser,
            Role.Contributor => permission.CanContributor,
            Role.BlogOwner => permission.CanBlogOwner,
            //_ => false
            _ => permission.CanUser
        };
    }
}

