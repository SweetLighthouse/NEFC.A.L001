using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FA.Domain.Entities;
using FA.Infrastructure.Context;
using FA.Application.Dtos.Categories;
using AutoMapper;
using FA.Domain.Enumerations;
using System.Security.Claims;
using FA.Application.Services;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AuthorizerService _authorizerService;
    private readonly MainDbContext _context;
    private readonly IMapper _mapper;

    public CategoriesController(AuthorizerService authorizeService, MainDbContext context, IMapper mapper)
    {
        _authorizerService = authorizeService;
        _context = context;
        _mapper = mapper;
    }



    // GET: api/Categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GeneralResponseCategoryDto>>> GetCategories()
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (_authorizerService.HasPermission(userRole, ModuleAction.Category.Index)) return Forbid();

        List<Category> Categories = await _context.Categories.Where(Category => !Category.IsDeleted).ToListAsync();
        var CategoryDtos = _mapper.Map<IEnumerable<GeneralResponseCategoryDto>>(Categories);
        return Ok(CategoryDtos);
    }

    

    // GET: api/Categories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DetailedResponseCategoryDto>> GetCategory(Guid id)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (_authorizerService.HasPermission(userRole, ModuleAction.Category.Details)) return Forbid();

        Category? Category = await _context.Categories.FirstOrDefaultAsync(Category => Category.Id == id && !Category.IsDeleted);
        if (Category == null)
        {
            return NotFound();
        }

        var CategoryDto = _mapper.Map<DetailedResponseCategoryDto>(Category);
        return Ok(CategoryDto);
    }

    // POST: api/Categories
    [HttpPost]
    public async Task<ActionResult> PostCategory(RequestCategoryDto CategoryDto)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (_authorizerService.HasPermission(userRole, ModuleAction.Category.Create)) return Forbid();

        Category Category = _mapper.Map<Category>(CategoryDto);
        _context.Categories.Add(Category);
        int affectedRows = await _context.SaveChangesAsync();
        if (affectedRows > 0)
        {
            return Created();
        }
        return BadRequest();
    }

    // PUT: api/Categories/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(Guid id, RequestCategoryDto CategoryDto)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (_authorizerService.HasPermission(userRole, ModuleAction.Category.Update)) return Forbid();

        var Category = await _context.Categories.FindAsync(id);
        if (Category == null)
        {
            return NotFound();
        }

        _mapper.Map(CategoryDto, Category);

        try
        {
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict();
        }
    }

    // DELETE: api/Categories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (_authorizerService.HasPermission(userRole, ModuleAction.Category.Delete)) return Forbid();

        var Category = await _context.Categories.FirstOrDefaultAsync(Category => Category.Id == id && !Category.IsDeleted);
        if (Category == null)
        {
            return NotFound();
        }

        Category.IsDeleted = true;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
