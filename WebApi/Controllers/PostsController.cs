using AutoMapper;
using AutoMapper.QueryableExtensions;
using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.PostDtos;
using FA.Application.Dtos.TagDtos;
using FA.Application.Services;
using FA.Domain.Entities;
using FA.Domain.Enumerations;
using FA.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController(MainDbContext context, IMapper mapper)
    : BaseEntitiesController<Post, PostCreateDto, PostUpdateDto, PostIndexDto, PostDetailDto>(context, mapper)
{

    [HttpPost]
    [Permission(ModuleAction.CreatePost)]
    public override async Task<IActionResult> PostAsync([FromBody] PostCreateDto requestDto)
    {
        // required fields
        if (requestDto is null ||
            string.IsNullOrWhiteSpace(requestDto.Title) ||
            string.IsNullOrWhiteSpace(requestDto.Content) ||
            requestDto.CategoryIds is null ||
            requestDto.TagNames is null) return BadRequest("Missing required fields");

        // check for valild id
        bool isValid = await _context.Blogs.AnyAsync(blog => blog.Id == requestDto.BlogId && !blog.IsDeleted) &&
            await _context.Categories.CountAsync(category => requestDto.CategoryIds.Contains(category.Id) && !category.IsDeleted) == requestDto.CategoryIds.Count;
        if (!isValid) return BadRequest("Blog or Categories not found.");

        // fill the object
        Post post = _mapper.Map<Post>(requestDto);

        // fill the category
        post.PostCategories = requestDto.CategoryIds
            .Select(categoryId => new PostCategory
            {
                CategoryId = categoryId,
                Post = post
            })
            .ToList();

        // all the requested tag names
        // already existed tags
        List<Tag> existTags = await _context.Tags
            .Where(tag => requestDto.TagNames.Contains(tag.Name)).ToListAsync();

        // make sure EF doesn't try to insert existing tags
        foreach (var tag in existTags)
        {
            _context.Attach(tag);
        }

        // 
        IEnumerable<Tag> newTags = requestDto.TagNames
            .Except(existTags.Select(tag => tag.Name))
            .Select(s => new Tag { Name = s }); // -> [] (none)

        post.Tags = existTags.Concat(newTags).ToList();

        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        //return StatusCode(201);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = post.Id }, post);
    }

    [HttpPut("{id}")]
    [Permission(ModuleAction.UpdatePost)]
    public override async Task<IActionResult> PutAsync(Guid id, [FromBody] PostUpdateDto requestDto)
    {
        if (requestDto is null) return BadRequest();
        Post? entity = await _dbSet.FindAsync(id);
        if (entity == null) return NotFound();
        if (Math.Abs((entity.UpdatedAt - requestDto.UpdatedAt).TotalSeconds) >= 1) return Conflict();

        if (requestDto.Title != null)
        {
            if (string.IsNullOrWhiteSpace(requestDto.Title)) return BadRequest();
            entity.Title = requestDto.Title;
        }

        if (requestDto.Content != null)
        {
            //if (string.IsNullOrWhiteSpace(requestDto.Content)) return BadRequest();
            entity.Content = requestDto.Content;
        }

        if (requestDto.BlogId != null)
        {
            if (!await _context.Blogs.AnyAsync(blog => blog.Id == requestDto.BlogId && !blog.IsDeleted)) return BadRequest();
            entity.BlogId = requestDto.BlogId.Value;
        }

        if (requestDto.CategoryIds != null)
        {
            if (await _context.Categories.CountAsync(category => requestDto.CategoryIds.Contains(category.Id)) != requestDto.CategoryIds.Count) return BadRequest();
            entity.PostCategories = requestDto.CategoryIds
            .Select(categoryId => new PostCategory
            {
                CategoryId = categoryId,
                Post = entity
            })
            .ToList();
        }

        if (requestDto.TagNames != null)
        {
            // already existed tags
            List<Tag> existTags = await _context.Tags
                .Where(tag => requestDto.TagNames.Contains(tag.Name)).ToListAsync();

            // make sure EF doesn't try to insert existing tags
            foreach (var tag in existTags)
            {
                _context.Attach(tag);
            }

            // 
            IEnumerable<Tag> newTags = requestDto.TagNames
                .Except(existTags.Select(tag => tag.Name))
                .Select(s => new Tag { Name = s });

            entity.Tags = existTags.Concat(newTags).ToList();
        }

        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    //[HttpPut]
    //[Permission(ModuleAction.UpdatePost)]
    //public override async Task<IActionResult> PutAsync(Guid id, [FromBody] PostRequestDto requestDto)
    //{
    //    Post? entity = await _dbSet.FindAsync(id);
    //    if (entity == null) return NotFound();

    //    // validation
    //    bool isValid = true;
    //    if (requestDto.Title != null && string.IsNullOrWhiteSpace(requestDto.Title)) isValid = false;
    //    if (requestDto.Content != null && string.IsNullOrWhiteSpace(requestDto.Content)) isValid = false;
    //    if (requestDto.BlogId != null)
    //    {
    //        isValid = await _context.Blogs.AnyAsync(blog => blog.Id == requestDto.BlogId && !blog.IsDeleted);
    //    }
    //    if (requestDto.CategoryIds != null)
    //    {
    //        isValid = await _context.Categories.CountAsync(category => requestDto.CategoryIds.Contains(category.Id) && !category.IsDeleted) == requestDto.CategoryIds.Count;
    //    }
    //    if (!isValid) return BadRequest("Blog or Categories not found or Invalid required fields.");

    //    _mapper.Map(requestDto, entity);
    //    entity.PostCategories = requestDto.CategoryIds
    //        .Select(categoryId => new PostCategory
    //        {
    //            CategoryId = categoryId,
    //            Post = entity
    //        })
    //        .ToList();

    //    //_dbSet.Update(entity);
    //    //await _context.SaveChangesAsync();
    //    //return NoContent();
    //}
}
