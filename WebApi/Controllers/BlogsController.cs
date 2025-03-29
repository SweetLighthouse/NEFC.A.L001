using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.BlogDtos;
using FA.Application.Services;
using FA.Domain.Entities;
using FA.Domain.Enumerations;
using FA.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsController(MainDbContext context, IMapper mapper)
    : BaseEntitiesController<Blog, BlogCreateDto, BlogUpdateDto, BlogIndexDto, BlogDetailDto>(context, mapper)
{

    [Permission(ModuleAction.IndexBlog)]
    public override async Task<ActionResult<PageResultDto<BlogIndexDto>>> GetsAsync(int page = 1, int pageSize = 10) => await base.GetsAsync(page, pageSize);


    [Permission(ModuleAction.DetailsBlog)]
    public override async Task<ActionResult<BlogDetailDto>> GetByIdAsync(Guid id) => await base.GetByIdAsync(id);


    [Permission(ModuleAction.CreateBlog)]
    public override async Task<IActionResult> PostAsync([FromBody] BlogCreateDto requestDto)
    {
        if (requestDto == null ||
            string.IsNullOrWhiteSpace(requestDto.Name)) return BadRequest();
        return await base.PostAsync(requestDto);
    }

    [HttpPut("{id}")]
    [Permission(ModuleAction.UpdateBlog)]
    public override async Task<IActionResult> PutAsync(Guid id, [FromBody] BlogUpdateDto requestDto)
    {
        if (requestDto == null) return BadRequest();
        Blog? entity = await _dbSet.FindAsync(id);
        if (entity == null) return NotFound();
        if (Math.Abs((entity.UpdatedAt - requestDto.UpdatedAt).TotalSeconds) >= 1) return Conflict();

        if (requestDto.Name != null)
        {
            if (string.IsNullOrWhiteSpace(requestDto.Name)) return BadRequest();
            entity.Name = requestDto.Name;
        }
        if(requestDto.Description != null) entity.Description = requestDto.Description;

        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Permission(ModuleAction.DeleteBlog)]
    public override async Task<IActionResult> DeleteAsync(Guid id)
    {
        // customized so every post is deleted together
        Blog? blog = await _dbSet
            .Include(b => b.Posts)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (blog == null) return NotFound();

        blog.IsDeleted = true;
        foreach (var post in blog.Posts) post.IsDeleted = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
