using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.Comments;
using FA.Application.Services;
using FA.Domain.Entities;
using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : GenericController<Comment, RequestCommentDto, ResponseCommentDto>
{
    private readonly AuthorizerService _authorizerService;
    private string? UserRole => User.FindFirst(ClaimTypes.Role)?.Value;
    public CommentsController(CommentService commentService, AuthorizerService authorizerService) : base(commentService)
    {
        _authorizerService = authorizerService;
    }

    public override async Task<ActionResult<PageResultDto<ResponseCommentDto>>> GetsAsync(int page = 1, int pageSize = 10)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.IndexComment)) return Forbid();
        return await base.GetsAsync(page, pageSize);
    }

    public override async Task<ActionResult<ResponseCommentDto>> GetAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DetailsComment)) return Forbid();
        return await base.GetAsync(id);
    }

    public override async Task<ActionResult> PostAsync([FromBody] RequestCommentDto requestTDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.CreateComment)) return Forbid();
        if (string.IsNullOrWhiteSpace(requestTDto.Content)) return BadRequest();

        return await base.PostAsync(requestTDto);
    }

    public override async Task<IActionResult> PutAsync(Guid id, [FromBody] RequestCommentDto requestTDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.UpdateComment)) return Forbid();
        if (string.IsNullOrWhiteSpace(requestTDto.Content)) return BadRequest();

        return await base.PutAsync(id, requestTDto);
    }

    public override async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.DeleteComment)) return Forbid();
        return await base.DeleteAsync(id);
    }
}
