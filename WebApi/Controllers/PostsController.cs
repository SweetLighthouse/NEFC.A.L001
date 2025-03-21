using Microsoft.AspNetCore.Mvc;
using FA.Application.Dtos.Posts;
using AutoMapper;
using FA.Application.Services;
using FA.Domain.Enumerations;
using FA.Domain.Entities;
using FA.Application.Dtos.BaseDtos;
using System.Security.Claims;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : GenericController<Post, RequestPostDto, ResponsePostDto>

{
    private readonly AuthorizerService _authorizerService;
    private string? UserRole => User.FindFirst(ClaimTypes.Role)?.Value;

    public PostsController(PostService postService, AuthorizerService authorizerService) : base(postService)
    {
        _authorizerService = authorizerService;
    }

    public override async Task<ActionResult<PageResultDto<ResponsePostDto>>> GetsAsync(int page = 1, int pageSize = 10)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.IndexPost)) return Forbid();
        return await base.GetsAsync(page, pageSize);
    }

    public override async Task<ActionResult<ResponsePostDto>> GetAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DetailsPost)) return Forbid();
        return await base.GetAsync(id);
    }

    public override async Task<ActionResult> PostAsync([FromBody] RequestPostDto requestTDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.CreatePost)) return Forbid();
        if (string.IsNullOrWhiteSpace(requestTDto.Title) || string.IsNullOrWhiteSpace(requestTDto.Content)) 
            return BadRequest();

        //if (requestTDto.BlogId == Guid.Empty || requestTDto.PostId == Guid.Empty) 
        //    return BadRequest();

        return await base.PostAsync(requestTDto);
    }

    public override async Task<IActionResult> PutAsync(Guid id, [FromBody] RequestPostDto requestTDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.UpdatePost)) return Forbid();
        if (string.IsNullOrWhiteSpace(requestTDto.Title) || string.IsNullOrWhiteSpace(requestTDto.Content))
            return BadRequest();

        //if (requestTDto.BlogId == Guid.Empty || requestTDto.PostId == Guid.Empty)
        //    return BadRequest();

        return await base.PutAsync(id, requestTDto);
    }

    public override async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DeletePost)) return Forbid();
        return await base.DeleteAsync(id);
    }
}
