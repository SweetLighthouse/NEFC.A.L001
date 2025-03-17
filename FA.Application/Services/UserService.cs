using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.Users;
using FA.Domain.Entities;
using FA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FA.Application.Services;

public class UserService
{
    private readonly MainDbContext _db;
    private readonly IMapper _mapper;

    public UserService(MainDbContext mainDbContext, IMapper mapper)
    {
        _db = mainDbContext;
        _mapper = mapper;
    }

    // thêm sửa xoá
    public async Task<bool> CreateUserAsync(RequestUserDto requestUserDto)
    {
        try
        {
            User user = _mapper.Map<User>(requestUserDto);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return true;
        } catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateUserAsync(Guid id, RequestUserDto requestUserDto)
    {
        try
        {
            User? user = await _db.Users.FindAsync(id);
            if (user == null) return false;

            _mapper.Map(requestUserDto, user);
            await _db.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        try
        {
            User? user = await _db.Users.FindAsync(id);
            if (user == null) return false;

            user.IsDeleted = true;
            await _db.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    // các kiểu get
    public async Task<DetailUserDto?> GetUserByIdAsync(Guid id)
    {
        User? user = await _db.Users.FindAsync(id);
        if (user == null || user.IsDeleted) return null;

        DetailUserDto detailUserDto = _mapper.Map<DetailUserDto>(user);
        return detailUserDto;
    }

    public async Task<PageResultDto<GeneralUserDto>> GetUsers(int page = 1, int pageSize = 10)
    {
        IQueryable<User> usersAll = _db.Users.Where(user => !user.IsDeleted);
        List<User> users = usersAll.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var totalItem = usersAll.Count();

        return new PageResultDto<GeneralUserDto>()
        {
            Page = page,
            PageSize = pageSize,
            TotalItem = totalItem,
            Items = _mapper.Map<List<GeneralUserDto>>(users)
        };
    }
}
