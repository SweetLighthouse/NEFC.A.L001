using AutoMapper;
using FA.Domain.Entities;
using FA.Infrastructure.Context;
using FA.Application.Dtos.Posts;
using Microsoft.EntityFrameworkCore;

namespace FA.Application.Services;

public class PostService : GenericService<Post, RequestPostDto, ResponsePostDto>
{
    public PostService(MainDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    //public override async Task<bool> AddAsync(RequestPostDto entityDto, Guid actionerId)
    //{
    //    try
    //    {
    //        Post entity = _mapper.Map<Post>(entityDto);
    //        entity.Tags = await _context.Tags.Where(tag => entityDto.TagIds.Contains(tag.Id)).ToListAsync();

    //        entity.CreatedBy = actionerId;
    //        entity.UpdatedBy = actionerId;

    //        await _dbSet.AddAsync(entity);
    //        await _context.SaveChangesAsync();
    //        return true;
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}

    //public override async Task<bool> UpdateAsync(Guid id, RequestPostDto entityDto, Guid actionerId)
    //{
    //    //base.UpdateAsync();
    //    try
    //    {
    //        Post? entity = await _dbSet.FindAsync(id);
    //        if (entity == null) return false;

    //        entity.UpdatedBy = actionerId;

    //        _mapper.Map(entityDto, entity);
    //        entity.Tags = await _context.Tags.Where(tag => entityDto.TagIds.Contains(tag.Id)).ToListAsync();

    //        await _context.SaveChangesAsync();
    //        return true;
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}
}
