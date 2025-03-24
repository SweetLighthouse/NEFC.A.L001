using AutoMapper;
using AutoMapper.QueryableExtensions;
using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.Users;
using FA.Application.Services;
using FA.Domain.Entities;
using FA.Domain.Enumerations;
using FA.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly MainDbContext _context;
    private readonly AuthorizerService _authorizerService;
    private readonly IMapper _mapper;

    private string? UserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    private string? UserRole => User.FindFirst(ClaimTypes.Role)?.Value;

    public UsersController(MainDbContext context, AuthorizerService authorizerService, IMapper mapper)
    {
        _mapper = mapper;
        _authorizerService = authorizerService;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PageResultDto<UserIndexDto>>> GetAsync(int page = 1, int pageSize = 10)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.IndexUser)) return Forbid();
        if (page < 1 || pageSize < 1) return BadRequest();
        IQueryable<User> query = _context.Users.Where(u => !u.IsDeleted);

        int totalItem = await query.CountAsync();

        List<UserIndexDto> userIndexDtos = await query
            .OrderBy(u => u.CreatedAt)
            .ProjectTo<UserIndexDto>(_mapper.ConfigurationProvider)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();

        return new PageResultDto<UserIndexDto>()
        {
            Items = userIndexDtos,
            Page = page,
            PageSize = pageSize,
            TotalItem = totalItem
        };
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailDto?>> Get(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.IndexUser)) return Forbid();
        return await _context.Users
            .Where(u => !u.IsDeleted && u.Id == id)
            .ProjectTo<UserDetailDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    // POST api/<UsersController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] UserRequestDto userRequestDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.CreateUser)) return Forbid();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (_context.Users.Any(u => u.Username == userRequestDto.Username)) return Conflict();
        var user = _mapper.Map<User>(userRequestDto);
        user.Password = BCrypt.Net.BCrypt.HashPassword(userRequestDto.Password);
        user.CreatorId = new Guid(UserId!);
        user.UpdatorId = new Guid(UserId!);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return StatusCode(201);
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] UserUpdateDto userUpdateDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.UpdateUser)) return Forbid();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        if (_context.Users.Any(u => u.Username == userUpdateDto.Username && u.Id != id)) return Conflict();
        _mapper.Map(userUpdateDto, user);
        user.UpdatorId = new Guid(UserId!);
        _context.Update(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("changepassword/{id}")] // any error?
    public async Task<ActionResult> PutPassword(Guid id, [FromBody] UserChangePasswordDto userChangePasswordDto)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.UpdateUser)) return Forbid();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        //if (!BCrypt.Net.BCrypt.Verify(userChangePasswordDto.OldPassword, user.Password)) return Unauthorized();
        user.Password = BCrypt.Net.BCrypt.HashPassword(userChangePasswordDto.NewPassword);
        user.UpdatorId = new Guid(UserId!);
        _context.Update(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    // how to specify the name?
    public async Task<ActionResult> Delete(Guid id)
    {
        if (!_authorizerService.HasPermission(UserRole, ModuleAction.UpdateUser)) return Forbid();
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        user.IsDeleted = true;
        user.UpdatorId = new Guid(UserId!);
        _context.Update(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
