using AutoMapper;
using FA.Domain.Entities;
using FA.Application.Dtos.Users;
using FA.Infrastructure.Context;
using FA.Application.Dtos.Accounts;
using Microsoft.EntityFrameworkCore;
using FA.Domain.Enumerations;

namespace FA.Application.Services;

public class UserService : GenericService<User, RequestUserDto, ResponseUserDto>
{
    public UserService(MainDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    private void HashPassword(RequestUserDto requestUserDto)
    {
        requestUserDto.Password = BCrypt.Net.BCrypt.HashPassword(requestUserDto.Password);
    }

    public override Task<bool> AddAsync(RequestUserDto requestDto, Guid actionerId)
    {
        HashPassword(requestDto);
        return base.AddAsync(requestDto, actionerId);
    }

    public override Task<bool> UpdateAsync(Guid id, RequestUserDto requestDto, Guid actionerId)
    {
        HashPassword(requestDto);
        return base.UpdateAsync(id, requestDto, actionerId);
    }

    public async Task<User?> FindUserAsync(LoginDto loginDto)
    {
        User? user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username);
        return user is not null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)
            ? user 
            : null;
    }

    public async Task<bool> AnyAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<User> RegisterAsync(RegisterDto registerDto)
    {
        User user = new();
        _mapper.Map(registerDto, user);
        Guid guid = Guid.NewGuid();
        user.Id = guid;
        user.CreatedBy = guid;
        user.UpdatedBy = guid;
        user.Role = Role.User;
        await _context.Users.AddAsync(user);
        return user;
    }
}
