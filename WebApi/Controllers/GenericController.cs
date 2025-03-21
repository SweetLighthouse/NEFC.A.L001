using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using FA.Application.Services;
using FA.Domain.Entities;
using FA.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public abstract class GenericController<TBase, TRequestDto, TResponseDto> : ControllerBase
    where TBase : class, IId, IMetadata, IIsDeleted
    where TRequestDto : class
    where TResponseDto : class, IId, IMetadata
{
    private readonly GenericService<TBase, TRequestDto, TResponseDto> _genericService;

    private Guid UserId => new(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

    public GenericController(GenericService<TBase, TRequestDto, TResponseDto> genericService)
    {
        _genericService = genericService;
    }

    // GET: api/<Entities>
    [HttpGet]
    public virtual async Task<ActionResult<PageResultDto<TResponseDto>>> GetsAsync(int page = 1, int pageSize = 10)
    {
        if (page < 1 || pageSize < 1) return BadRequest("page and pageSize must be greater than zero.");
        PageResultDto<TResponseDto>? pageResultDto = await _genericService.GetsAsync(page, pageSize);
        return Ok(pageResultDto);
    }

    // GET: api/<Entities>/5
    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TResponseDto>> GetAsync(Guid id)
    {
        TResponseDto? detailDto = await _genericService.GetByIdAsync(id);
        return detailDto != null ? Ok(detailDto) : NotFound();
    }

    // POST: api/<Entities>
    [HttpPost]
    public virtual async Task<ActionResult> PostAsync([FromBody] TRequestDto requestDto)
    {
        bool success = await _genericService.AddAsync(requestDto, UserId); 
        return success ? Created() : BadRequest();
    }

    // PUT: api/<Entities>/5
    [HttpPut("{id}")]
    public virtual async Task<IActionResult> PutAsync(Guid id, [FromBody] TRequestDto requestDto)
    {
        bool success = await _genericService.UpdateAsync(id, requestDto, UserId);
        return success ? NoContent() : BadRequest();
    }

    // DELETE: api/<Entities>/5
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
    {
        bool success = await _genericService.DeleteAsync(id, UserId);
        return success ? NoContent() : BadRequest();
    }
}