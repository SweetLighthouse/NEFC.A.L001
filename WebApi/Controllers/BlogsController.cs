using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FA.Domain.Entities;
using FA.Infrastructure.Context;
using FA.Application.Dtos.Blogs;
using AutoMapper;
using System.Security.Claims;
using FA.Application.Services;
using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsController : ControllerBase
{
    private readonly AuthorizerService _authorizerService;
    private readonly MainDbContext _context;
    private readonly IMapper _mapper;

    public BlogsController(AuthorizerService authorizerService, MainDbContext context, IMapper mapper)
    {
        _authorizerService = authorizerService;
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Blogs
    [HttpGet]
    //[Authorize(Role="Admin")]
    public async Task<ActionResult<IEnumerable<GeneralResponseBlogDto>>> GetBlogs()
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (!_authorizerService.HasPermission(userRole, ModuleAction.Blog.Index)) return Forbid();

        List<Blog> blogs = await _context.Blogs.Where(blog => !blog.IsDeleted).ToListAsync();
        var blogDtos = _mapper.Map<IEnumerable<GeneralResponseBlogDto>>(blogs);
        return Ok(blogDtos);
    }

    // GET: api/Blogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DetailedResponseBlogDto>> GetBlog(Guid id)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (!_authorizerService.HasPermission(userRole, ModuleAction.Blog.Details)) return Forbid();

        Blog? blog = await _context.Blogs.FirstOrDefaultAsync(blog => blog.Id == id && !blog.IsDeleted);
        if (blog == null)
        {
            return NotFound();
        }

        var blogDto = _mapper.Map<DetailedResponseBlogDto>(blog);
        return Ok(blogDto);
    }

    // POST: api/Blogs
    [HttpPost]
    public async Task<ActionResult> PostBlog(RequestBlogDto blogDto)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (!_authorizerService.HasPermission(userRole, ModuleAction.Blog.Create)) return Forbid();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // random people cannot create new blog
        if (userId == null)
        {
            return Unauthorized();
        }

        Blog blog = _mapper.Map<Blog>(blogDto);
        blog.CreatedBy = Guid.Parse(userId);
        blog.UpdatedBy = Guid.Parse(userId);

        _context.Blogs.Add(blog);
        int affectedRows = await _context.SaveChangesAsync();
        if (affectedRows > 0)
        {
            return CreatedAtAction(nameof(GetBlog), new { id = blog.Id });
        }
        return BadRequest();
    }

    // PUT: api/Blogs/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlog(Guid id, RequestBlogDto blogDto)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (!_authorizerService.HasPermission(userRole, ModuleAction.Blog.Update)) return Forbid();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }

        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null)
        {
            return NotFound();
        }

        if (blog.CreatedBy != Guid.Parse(userId))
        {
            return Unauthorized();
        }

        _mapper.Map(blogDto, blog);

        try
        {
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBlog), new { id = blog.Id });
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict();
        }
    }

    // DELETE: api/Blogs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog(Guid id)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (!_authorizerService.HasPermission(userRole, ModuleAction.Blog.Delete)) return Forbid();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var blog = await _context.Blogs.FirstOrDefaultAsync(blog => blog.Id == id && !blog.IsDeleted);
        if (blog == null) return NotFound();

        if (blog.CreatedBy != Guid.Parse(userId)) return Unauthorized();

        blog.IsDeleted = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
