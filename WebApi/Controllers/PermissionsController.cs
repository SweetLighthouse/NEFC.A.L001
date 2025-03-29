using FA.Application.Dtos.Permissions;
using FA.Application.Services;
using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionsController() : ControllerBase
{
    [HttpGet]
    public ActionResult<List<PermissionDto>> GetPermissions()
    {
        List<PermissionDto> permissionsList = [];
        foreach (var entry in Permission.permissionsTable)
        {
            permissionsList.Add(new PermissionDto
            {
                ModuleAction = entry.Key.ToString(),
                CanUser = entry.Value[(int)Role.User] == 'X',
                CanContributor = entry.Value[(int)Role.Contributor] == 'X',
                CanBlogOwner = entry.Value[(int)Role.BlogOwner] == 'X'
            });
        }
        return Ok(permissionsList);
    }

}
