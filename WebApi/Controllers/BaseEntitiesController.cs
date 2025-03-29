using AutoMapper;
using AutoMapper.QueryableExtensions;
using FA.Application.Dtos.BaseDtos;
using FA.Domain.Entities;
using FA.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

//[Route("api/[controller]")]
[ApiController]
public abstract class BaseEntitiesController<TBase, TCreateDto, TUpdateDto, TIndexDto, TDetailDto> : ControllerBase
    where TBase : BaseEntity
    where TCreateDto : class
    where TUpdateDto : LastUpdated
    where TIndexDto : BaseEntityDto
    where TDetailDto : BaseEntityDto
{
    protected readonly MainDbContext _context;
    protected readonly IMapper _mapper;
    protected readonly DbSet<TBase> _dbSet;

    public BaseEntitiesController(MainDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<TBase>();
    }

    // GET: api/<Entities>
    [HttpGet]
    public virtual async Task<ActionResult<PageResultDto<TIndexDto>>> GetsAsync(int page = 1, int pageSize = 10)
    {
        if (page < 1 || pageSize < 1) return BadRequest("page and pageSize must be greater than zero.");

        //IQueryable<TBase> query = _dbSet.Where(entity => !entity.IsDeleted);

        var totalItem = await _dbSet.CountAsync();

        List<TIndexDto> items = await _dbSet
            //.Where(entity => !entity.IsDeleted)
            .OrderByDescending(e => e.CreatedAt)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ProjectTo<TIndexDto>(_mapper.ConfigurationProvider)
            .ToListAsync(); // thisline

        return new PageResultDto<TIndexDto>()
        {
            Page = page,
            PageSize = pageSize,
            TotalItem = totalItem,
            Items = items
        };
    }


    // GET: api/<Entities>/5
    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TDetailDto>> GetByIdAsync(Guid id)
    {
        TDetailDto? entity = await _dbSet
            .ProjectTo<TDetailDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        if (entity == null) return NotFound();
        return entity;
    }

    // POST: api/<Entities>
    [HttpPost]
    public virtual async Task<IActionResult> PostAsync([FromBody] TCreateDto requestDto)
    {
        if (!ModelState.IsValid) return BadRequest();
        TBase entity = _mapper.Map<TBase>(requestDto);
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return StatusCode(201);
        //return CreatedAtAction(nameof(GetByIdAsync), new { id = entity.Id }, entity);
    }

    // PUT: api/<Entities>/5
    [HttpPut("{id}")]
    public virtual async Task<IActionResult> PutAsync(Guid id, [FromBody] TUpdateDto requestDto)
    {
        if (!ModelState.IsValid) return BadRequest();
        TBase? entity = await _dbSet.FindAsync(id);
        if (entity == null) return NotFound();
        if (Math.Abs((entity.UpdatedAt - requestDto.UpdatedAt).TotalSeconds) >= 1) return Conflict();
        _mapper.Map(requestDto, entity);
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/<Entities>/5
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
    {
        TBase? entity = await _dbSet.FindAsync(id);
        if (entity == null) return NotFound();
        entity.IsDeleted = true;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    //public async Task<ActionResult<PageResultDto<IndexBlogDto>>> FindBlogsAsync(
    //    DateTime? createdBefore,
    //    DateTime? createdAfter,
    //    DateTime? updatedBefore,
    //    DateTime? updatedAfter,
    //    Guid? createdBy,
    //    Guid? updatedBy,
    //    string? name,
    //    string? orderBy,
    //    int page = 1, int pageSize = 10)
    //{
    //    if (!_authorizerService.HasPermission(UserRole, ModuleAction.IndexBlog)) return Forbid();
    //    if (page < 1 || pageSize < 1) return BadRequest("postPage and postPageSize must be greater than zero.");

    //    IQueryable<Blog> query = _blogService.Query;
    //    if (createdBefore is not null) query = query.Where(blog => blog.CreatedAt > createdBefore);
    //    if (createdAfter is not null) query = query.Where(blog => blog.CreatedAt < createdAfter);
    //    if (updatedBefore is not null) query = query.Where(blog => blog.CreatedAt > updatedBefore);
    //    if (updatedAfter is not null) query = query.Where(blog => blog.CreatedAt < updatedAfter);
    //    if (createdBy is not null) query = query.Where(blog => blog.CreatedById == createdBy);
    //    if (updatedBy is not null) query = query.Where(blog => blog.UpdatedById == updatedBy);
    //    if (name is not null) query = query.Where(blog => blog.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

    //    query = orderBy switch
    //    {
    //        nameof(Blog.CreatedAt) => query.OrderBy(blog => blog.CreatedAt),
    //        nameof(Blog.UpdatedAt) => query.OrderBy(blog => blog.UpdatedAt),
    //        _ => query.OrderBy(blog => blog.Id)
    //    };
}
