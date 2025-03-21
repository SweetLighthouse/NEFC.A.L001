using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using FA.Domain.Interfaces;
using FA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FA.Application.Services;

public class GenericService<TBase, TRequestDto, TResponseDto> 
    where TBase : class, IId, IMetadata, IIsDeleted
    where TRequestDto : class
    where TResponseDto : class, IId, IMetadata
{
    protected readonly MainDbContext _context;
    protected readonly IMapper _mapper;
    protected readonly DbSet<TBase> _dbSet;

    public GenericService(MainDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<TBase>();
    }

    // thêm sửa xoá
    public virtual async Task<bool> AddAsync(TRequestDto requestDto, Guid actionerId)
    {
        try
        {
            TBase entity = _mapper.Map<TBase>(requestDto);

            entity.CreatedBy = actionerId;
            entity.UpdatedBy = actionerId;

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        } catch
        {
            return false;
        }
    }

    public virtual async Task<bool> UpdateAsync(Guid id, TRequestDto requestDto, Guid actionerId)
    {
        try
        {
            TBase? entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            entity.UpdatedBy = actionerId;

            _mapper.Map(requestDto, entity);
            _dbSet.Update(entity); // Ensures EF tracks the changes
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public virtual async Task<bool> DeleteAsync(Guid id, Guid actionerId)
    {
        try
        {
            TBase? entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedBy = actionerId;
            _dbSet.Update(entity); // Ensures EF tracks the changes

            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    // các kiểu get
    public virtual async Task<TResponseDto?> GetByIdAsync(Guid id)
    {
        TBase? entity = await _dbSet.FindAsync(id);
        if (entity == null || entity.IsDeleted) return default;
        return _mapper.Map<TResponseDto>(entity);
    }

    public virtual async Task<PageResultDto<TResponseDto>> GetsAsync(int page = 1, int pageSize = 10)
    {
        if (page < 1 || pageSize < 1) throw new ArgumentOutOfRangeException("Page and PageSize must be greater than zero.");
        IQueryable<TBase> alls = _dbSet.Where(entity => !entity.IsDeleted);
        List<TBase> Blogs = await alls.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        var totalItem = await alls.CountAsync();

        return new PageResultDto<TResponseDto>()
        {
            Page = page,
            PageSize = pageSize,
            TotalItem = totalItem,
            Items = _mapper.Map<List<TResponseDto>>(Blogs)
        };
    }
}

