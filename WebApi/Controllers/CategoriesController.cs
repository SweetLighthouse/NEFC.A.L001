using Microsoft.AspNetCore.Mvc;
using FA.Application.Dtos.Categories;
using AutoMapper;
using FA.Application.Services;
using FA.Domain.Enumerations;
using FA.Domain.Entities;
using FA.Application.Dtos.BaseDtos;
using System.Security.Claims;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategorysController : GenericController<Category, RequestCategoryDto, ResponseCategoryDto>
{
    private readonly AuthorizerService _authorizerService;
    private string? UserRole => User.FindFirst(ClaimTypes.Role)?.Value;

    public CategorysController(AuthorizerService authorizerService, CategoryService CategoryService) : base(CategoryService)
    {
        _authorizerService = authorizerService;
    }

    public override async Task<ActionResult<PageResultDto<ResponseCategoryDto>>> GetsAsync(int page = 1, int pageSize = 10)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.IndexCategory)) return Forbid();
        return await base.GetsAsync(page, pageSize);
    }

    public override async Task<ActionResult<ResponseCategoryDto>> GetAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DetailsCategory)) return Forbid();
        return await base.GetAsync(id);
    }

    public override async Task<ActionResult> PostAsync([FromBody] RequestCategoryDto requestTDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.CreateCategory)) return Forbid();
        if (string.IsNullOrWhiteSpace(requestTDto.Name)) return BadRequest();

        return await base.PostAsync(requestTDto);
    }

    public override async Task<IActionResult> PutAsync(Guid id, [FromBody] RequestCategoryDto requestTDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.UpdateCategory)) return Forbid();
        if (string.IsNullOrWhiteSpace(requestTDto.Name)) return BadRequest();

        return await base.PutAsync(id, requestTDto);
    }

    public override async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DeleteCategory)) return Forbid();
        return await base.DeleteAsync(id);
    }
}
