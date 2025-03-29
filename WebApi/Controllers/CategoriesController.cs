using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.CategoryDtos;
using FA.Application.Services;
using FA.Domain.Entities;
using FA.Domain.Enumerations;
using FA.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(MainDbContext context, IMapper mapper) 
    : BaseEntitiesController<Category, CategoryCreateDto, CategoryUpdateDto, CategoryIndexDto, CategoryDetailDto>(context, mapper)
{

    [Permission(ModuleAction.IndexCategory)]
    public override async Task<ActionResult<PageResultDto<CategoryIndexDto>>> GetsAsync(int page = 1, int pageSize = 10) 
        => await base.GetsAsync(page, pageSize);


    [Permission(ModuleAction.DetailsCategory)]
    public override async Task<ActionResult<CategoryDetailDto>> GetByIdAsync(Guid id) => await base.GetByIdAsync(id);


    [Permission(ModuleAction.CreateCategory)]
    public override async Task<IActionResult> PostAsync([FromBody] CategoryCreateDto requestDto)
    {
        if (requestDto == null ||
            string.IsNullOrWhiteSpace(requestDto.Name)) return BadRequest();
        return await base.PostAsync(requestDto);
    }

    [HttpPut("{id}")]
    [Permission(ModuleAction.UpdateCategory)]
    public override async Task<IActionResult> PutAsync(Guid id, [FromBody] CategoryUpdateDto requestDto)
    {
        if (requestDto == null) return BadRequest();
        Category? entity = await _dbSet.FindAsync(id);
        if (entity == null) return NotFound();
        if (Math.Abs((entity.UpdatedAt - requestDto.UpdatedAt).TotalSeconds) >= 1) return Conflict();
        if (requestDto.Name != null)
        {
            if (string.IsNullOrWhiteSpace(requestDto.Name)) return BadRequest();
            entity.Name = requestDto.Name;
        }
        if (requestDto.Description != null) entity.Description = requestDto.Description;

        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [Permission(ModuleAction.DeleteCategory)]
    public override async Task<IActionResult> DeleteAsync(Guid id) => await base.DeleteAsync(id);
}
