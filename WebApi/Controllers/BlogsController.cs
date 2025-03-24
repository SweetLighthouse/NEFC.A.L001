using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.Blogs;
using FA.Application.Services;
using FA.Domain.Entities;
using FA.Domain.Enumerations;
using FA.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsController : IndependentEntityController<Blog, BlogRequestDto, BlogIndexDto, BlogDetailDto>
{
    private readonly string? UserRole;
    private readonly AuthorizerService _authorizerService;
    public BlogsController(MainDbContext context, IMapper mapper, AuthorizerService authorizerService) : base(context, mapper)
    {
        _authorizerService = authorizerService;
        UserRole = User.FindFirst(ClaimTypes.Role)?.Value;
    }

    public override async Task<ActionResult<PageResultDto<BlogIndexDto>>> GetsAsync(int page = 1, int pageSize = 10)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.IndexBlog)) return Forbid();
        return await base.GetsAsync(page, pageSize);
    }

    public override async Task<ActionResult<BlogDetailDto>> GetByIdAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DetailsBlog)) return Forbid();
        return await base.GetByIdAsync(id);
    }

    public override async Task<ActionResult> PostAsync([FromBody] BlogRequestDto requestDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.CreateBlog)) return Forbid();
        return await base.PostAsync(requestDto);
    }

    public override async Task<IActionResult> PutAsync(Guid id, [FromBody] BlogRequestDto requestDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.UpdateBlog)) return Forbid();
        return await base.PutAsync(id, requestDto);
    }

    public override async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DeleteBlog)) return Forbid();
        return await base.DeleteAsync(id);
    }


}
