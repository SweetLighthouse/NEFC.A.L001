using Microsoft.AspNetCore.Mvc;
using FA.Application.Dtos.Blogs;
using AutoMapper;
using FA.Application.Services;
using FA.Domain.Enumerations;
using FA.Domain.Entities;
using FA.Application.Dtos.BaseDtos;
using System.Security.Claims;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsController : GenericController<Blog, RequestBlogDto, ResponseBlogDto>
{
    private readonly AuthorizerService _authorizerService;
    private string? UserRole => User.FindFirst(ClaimTypes.Role)?.Value;

    public BlogsController(BlogService blogService, AuthorizerService authorizerService) : base(blogService)
    {
        _authorizerService = authorizerService;
    }

    public override async Task<ActionResult<PageResultDto<ResponseBlogDto>>> GetsAsync(int page = 1, int pageSize = 10)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.IndexBlog)) return Forbid();
        return await base.GetsAsync(page, pageSize);
    }

    public override async Task<ActionResult<ResponseBlogDto>> GetAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DetailsBlog)) return Forbid();
        return await base.GetAsync(id);
    }

    public override async Task<ActionResult> PostAsync([FromBody] RequestBlogDto requestTDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.CreateBlog)) return Forbid();
        if (string.IsNullOrWhiteSpace(requestTDto.Name)) return BadRequest();

        return await base.PostAsync(requestTDto);
    }

    public override async Task<IActionResult> PutAsync(Guid id, [FromBody] RequestBlogDto requestTDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.UpdateBlog)) return Forbid();
        if (string.IsNullOrWhiteSpace(requestTDto.Name)) return BadRequest();

        return await base.PutAsync(id, requestTDto);
    }

    public override async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DeleteBlog)) return Forbid();
        return await base.DeleteAsync(id);
    }
}
