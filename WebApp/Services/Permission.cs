using FA.Application.Dtos.Permissions;
using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using WebApp.Commons;

namespace WebApp.Services;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
sealed class PermissionAttribute(ModuleAction moduleAction) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        PermissionDto? permission = PermissionTable.Permissions.FirstOrDefault(p => p.ModuleAction == moduleAction.ToString());
        if (permission == null)
        {
            context.Result = new ViewResult
            {
                ViewName = Constants.ViewName.NotFound
            };
            return;
        }

        string? roleRaw = context.HttpContext.User?.FindFirst(ClaimTypes.Role)?.Value;
        Enum.TryParse(roleRaw, out Role role);

        bool hasPermission = role switch
        {
            Role.User => permission.CanUser,
            Role.Contributor => permission.CanContributor,
            Role.BlogOwner => permission.CanBlogOwner,
            //_ => false
            _ => permission.CanUser
        };

        if (!hasPermission)
        {
            context.Result = new ViewResult
            {
                ViewName = Constants.ViewName.Forbidden
            };
            return;
        }
    }
}
