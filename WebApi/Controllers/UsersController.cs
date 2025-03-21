using Microsoft.AspNetCore.Mvc;
using FA.Application.Dtos.Users;
using FA.Application.Services;
using FA.Domain.Enumerations;
using FA.Domain.Entities;
using FA.Application.Dtos.BaseDtos;
using System.Security.Claims;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : GenericController<User, RequestUserDto, ResponseUserDto>

{
    private readonly AuthorizerService _authorizerService;
    private string? UserRole => User.FindFirst(ClaimTypes.Role)?.Value;

    public UsersController(AuthorizerService authorizerService, UserService UserService) : base(UserService)
    {
        _authorizerService = authorizerService;
    }

    public override async Task<ActionResult<PageResultDto<ResponseUserDto>>> GetsAsync(int page = 1, int pageSize = 10)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.IndexUser)) return Forbid();
        return await base.GetsAsync(page, pageSize);
    }

    public override async Task<ActionResult<ResponseUserDto>> GetAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DetailsUser)) return Forbid();
        return await base.GetAsync(id);
    }

    public override async Task<ActionResult> PostAsync([FromBody] RequestUserDto requestTDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.CreateUser)) return Forbid();
        if (!_authorizerService.IsValidUsername(requestTDto.Username) 
            || !_authorizerService.IsValidPassword(requestTDto.Password)) return BadRequest();

        return await base.PostAsync(requestTDto);
    }

    public override async Task<IActionResult> PutAsync(Guid id, [FromBody] RequestUserDto requestTDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.UpdateUser)) return Forbid();
        if (!_authorizerService.IsValidUsername(requestTDto.Username)
            || !_authorizerService.IsValidPassword(requestTDto.Password)) return BadRequest();

        return await base.PutAsync(id, requestTDto);
    }

    public override async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DeleteUser)) return Forbid();
        return await base.DeleteAsync(id);
    }
}
