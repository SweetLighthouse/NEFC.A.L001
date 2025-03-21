using Microsoft.AspNetCore.Mvc;
using FA.Application.Dtos.Tags;
using AutoMapper;
using FA.Application.Services;
using FA.Domain.Enumerations;
using FA.Domain.Entities;
using FA.Application.Dtos.BaseDtos;
using System.Security.Claims;
using FA.Application.Dtos.Posts;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : GenericController<Tag, RequestTagDto, ResponseTagDto>
{
    private readonly AuthorizerService _authorizerService;
    private string? UserRole => User.FindFirst(ClaimTypes.Role)?.Value;

    public TagsController(AuthorizerService authorizerService, TagService TagService) : base(TagService)
    {
        _authorizerService = authorizerService;
    }

    public override async Task<ActionResult<PageResultDto<ResponseTagDto>>> GetsAsync(int page = 1, int pageSize = 10)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.IndexTag)) return Forbid();
        return await base.GetsAsync(page, pageSize);
    }

    public override async Task<ActionResult<ResponseTagDto>> GetAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DetailsTag)) return Forbid();
        return await base.GetAsync(id);
    }

    public override async Task<ActionResult> PostAsync([FromBody] RequestTagDto requestTDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.CreateTag)) return Forbid();
        if (string.IsNullOrWhiteSpace(requestTDto.Name)) return BadRequest();

        return await base.PostAsync(requestTDto);
    }

    public override async Task<IActionResult> PutAsync(Guid id, [FromBody] RequestTagDto requestTDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.UpdateTag)) return Forbid();
        if (string.IsNullOrWhiteSpace(requestTDto.Name)) return BadRequest();

        return await base.PutAsync(id, requestTDto);
    }

    public override async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DeleteTag)) return Forbid();
        return await base.DeleteAsync(id);
    }
}
