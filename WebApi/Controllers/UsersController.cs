using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.Users;
using FA.Application.Services;
using FA.Domain.Entities;
using FA.Domain.Enumerations;
using FA.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(MainDbContext context, IMapper mapper) 
    : BaseEntitiesController<User, UserCreateDto, UserUpdateDto, UserIndexDto, UserDetailDto>(context, mapper)
{

    [Permission(ModuleAction.IndexUser)]
    public override async Task<ActionResult<PageResultDto<UserIndexDto>>> GetsAsync(int page = 1, int pageSize = 10) 
        => await base.GetsAsync(page, pageSize);


    [Permission(ModuleAction.DetailsUser)]
    public override async Task<ActionResult<UserDetailDto>> GetByIdAsync(Guid id) => await base.GetByIdAsync(id);


    [HttpPost]
    [Permission(ModuleAction.CreateUser)]
    public override async Task<IActionResult> PostAsync([FromBody] UserCreateDto userRequestDto)
    {        
        if (string.IsNullOrWhiteSpace(userRequestDto.Username)
            || string.IsNullOrWhiteSpace(userRequestDto.Password)
            || !Enum.IsDefined(userRequestDto.Role)) return BadRequest();

        if (_context.Users.Any(u => u.Username == userRequestDto.Username)) return Conflict();
        
        // reimplement from base
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = _mapper.Map<User>(userRequestDto);
        user.Password = BCrypt.Net.BCrypt.HashPassword(userRequestDto.Password);

        await _dbSet.AddAsync(user);
        await _context.SaveChangesAsync();
        //return StatusCode(201);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = user.Id}, user);
    }

    [HttpPut("{id}")]
    [Permission(ModuleAction.UpdateUser)]
    public override async Task<IActionResult> PutAsync(Guid id, [FromBody] UserUpdateDto requestDto)
    {
        if (_context.Users.Any(u => u.Username == requestDto.Username && u.Id != id)) return Conflict();

        User? user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        if (Math.Abs((user.UpdatedAt - requestDto.UpdatedAt).TotalSeconds) >= 1) return Conflict();

        if (requestDto.Username != null) user.Username = requestDto.Username;
        if (requestDto.Password != null) user.Password = BCrypt.Net.BCrypt.HashPassword(requestDto.Password);
        if (requestDto.Role != null) user.Role = requestDto.Role.Value;
        user.Email = requestDto.Email;
        user.About = requestDto.About;

        // reimplement from base
        _context.Update(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }


    [Permission(ModuleAction.DeleteUser)]
    public override async Task<IActionResult> DeleteAsync(Guid id) => await base.DeleteAsync(id);
}
