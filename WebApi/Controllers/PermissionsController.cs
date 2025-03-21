using FA.Application.Dtos.Permissions;
using FA.Application.Services;
using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionsController : ControllerBase
{
    private readonly AuthorizerService _authorizerService;

    public PermissionsController(AuthorizerService authorizerService)
    {
        _authorizerService = authorizerService;
    }


    [HttpGet]
    public ActionResult<List<PermissionDto>> GetPermissions()
    {
        List<PermissionDto> permissionsList = new List<PermissionDto>();

        foreach (var entry in _authorizerService.GetPermissionsTable())
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
