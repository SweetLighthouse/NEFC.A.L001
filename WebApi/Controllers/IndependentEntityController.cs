using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using FA.Domain.Entities;
using FA.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebApi.Controllers;

//[Route("api/[controller]")]
[ApiController]
public abstract class IndependentEntityController<TBase, TRequestDto, TIndexDto, TDetailDto> : ControllerBase
    where TBase : BaseEntity
    where TRequestDto : class
    where TIndexDto : class
    where TDetailDto : class
{
    private readonly string? UserId;
    //private readonly string? UserRole;

    protected readonly MainDbContext _context;
    protected readonly IMapper _mapper;
    protected readonly DbSet<TBase> _dbSet;

    public IndependentEntityController(MainDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<TBase>();
        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //UserRole = User.FindFirst(ClaimTypes.Role)?.Value;
    }


    private async Task<PageResultDto<TIndexDto>> GetsAsyncService(int page = 1, int pageSize = 10)
    {
        IQueryable<TBase> allItems = _dbSet.Where(entity => !entity.IsDeleted).OrderByDescending(e => e.CreatedAt);
        var totalItem = await allItems.CountAsync();
        List<TBase> items = await allItems.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PageResultDto<TIndexDto>()
        {
            Page = page,
            PageSize = pageSize,
            TotalItem = totalItem,
            Items = _mapper.Map<List<TIndexDto>>(items)
        };
    }

    // GET: api/<Entities>
    [HttpGet]
    public virtual async Task<ActionResult<PageResultDto<TIndexDto>>> GetsAsync(int page = 1, int pageSize = 10)
    {
        if (page < 1 || pageSize < 1) return BadRequest("page and pageSize must be greater than zero.");
        PageResultDto<TIndexDto>? pageResultDto = await GetsAsyncService(page, pageSize);
        return Ok(pageResultDto);
    }


    // GET: api/<Entities>/5
    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TDetailDto>> GetByIdAsync(Guid id)
    {
        TBase? entity = await _dbSet.FindAsync(id);
        if (entity == null || entity.IsDeleted) return NotFound();
        var detailDto = _mapper.Map<TDetailDto>(entity);
        return Ok(detailDto);
    }

    // POST: api/<Entities>
    [HttpPost]
    public virtual async Task<ActionResult> PostAsync([FromBody] TRequestDto requestDto)
    {
        try
        {
            if (UserId is null) return Unauthorized();

            TBase entity = _mapper.Map<TBase>(requestDto);

            entity.CreatorId = new Guid(UserId);
            entity.UpdatorId = new Guid(UserId);

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        catch
        {
            return BadRequest();
        }
    }

    // PUT: api/<Entities>/5
    [HttpPut("{id}")]
    public virtual async Task<IActionResult> PutAsync(Guid id, [FromBody] TRequestDto requestDto)
    {
        try
        {
            if (UserId is null) return Unauthorized();

            TBase? entity = await _dbSet.FindAsync(id);
            if (entity == null) return NotFound();

            entity.UpdatorId = new Guid(UserId);

            _mapper.Map(requestDto, entity);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    // DELETE: api/<Entities>/5
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            if (UserId is null) return Unauthorized();

            TBase? entity = await _dbSet.FindAsync(id);
            if (entity == null) return NotFound();

            entity.IsDeleted = true;
            entity.UpdatorId = new Guid(UserId);
            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
