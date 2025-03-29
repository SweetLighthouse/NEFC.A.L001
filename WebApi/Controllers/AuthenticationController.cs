using Microsoft.AspNetCore.Mvc;
using FA.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using FA.Domain.Enumerations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FA.Application.Dtos.AccountDtos;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly MainDbContext _mainDbContext;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthenticationController(MainDbContext mainDbContext, IConfiguration configuration, IMapper mapper)
    {
        _mainDbContext = mainDbContext;
        _configuration = configuration;
        _mapper = mapper;
    }

    [HttpPost(nameof(Register))]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        // if username already used
        bool existed = await _mainDbContext.Users.AnyAsync(u => u.Username == registerDto.Username);
        if (existed) return Conflict();

        Guid guid = Guid.NewGuid();
        User user = _mapper.Map<User>(registerDto);
        user.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        //user.Id = guid;
        //user.CreatorId = guid;
        //user.UpdatorId = guid;
        await _mainDbContext.Users.AddAsync(user);
        await _mainDbContext.SaveChangesAsync();
        return StatusCode(201); // return Created(); // will return 204 if calls with 0 args.
    }

    [HttpPost(nameof(Login))]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        // find user with username
        var user = await _mainDbContext.Users
            .Where(u => u.Username == loginDto.Username && !u.IsDeleted)
            .Select(u => new 
            {
                u.Id,
                u.Username,
                u.Password,
                u.Role
            })
            .SingleOrDefaultAsync();

        // if not found or password not match
        if(user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)) return NotFound();
        return Ok(GenerateToken(user.Id, user.Username, user.Role));
    }

    private string GenerateToken(Guid id, string username, Role role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            //Issuer = _configuration["JwtSettings:Issuer"],
            //Audience = _configuration["JwtSettings:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string? UserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    [HttpGet(nameof(MyAccount))]
    public async Task<ActionResult<AccountDetailDto>> MyAccount()
    {
        if (UserId is null) return Unauthorized();
        return await _mainDbContext.Users
            .Where(u => u.Id == new Guid(UserId) && !u.IsDeleted)
            .ProjectTo<AccountDetailDto>(_mapper.ConfigurationProvider)
            .SingleAsync();
    }


    [HttpPut(nameof(UpdateAccount))]
    public async Task<ActionResult> UpdateAccount(AccountUpdateDto accountUpdateDto)
    {
        if (UserId is null) return Unauthorized();
        if (!ModelState.IsValid) return BadRequest();
        User user = await _mainDbContext.Users
            .Where(u => u.Id == new Guid(UserId) && !u.IsDeleted)
            .SingleAsync();

        if (accountUpdateDto.UpdatedAt != user.UpdatedAt) return Conflict();

        if (!BCrypt.Net.BCrypt.Verify(accountUpdateDto.CurrentPassword, user.Password)) return Unauthorized();
        if (_mainDbContext.Users.Any(u => u.Username == accountUpdateDto.Username && u.Id != new Guid(UserId))) return Conflict();

        if (!string.IsNullOrWhiteSpace(accountUpdateDto.Username)) user.Username = accountUpdateDto.Username;
        if (accountUpdateDto.NewPassword != null) user.Password = BCrypt.Net.BCrypt.HashPassword(accountUpdateDto.NewPassword);
        user.Email = accountUpdateDto.Email;
        user.About = accountUpdateDto.About;
        //user.UpdatorId = user.Id;
        _mainDbContext.Update(user);
        await _mainDbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut(nameof(DeleteAccount))]
    public async Task<ActionResult> DeleteAccount(AccountDeleteDto accountDeleteDto)
    {
        if (UserId is null) return Unauthorized();
        if (!ModelState.IsValid) return BadRequest();
        User user = await _mainDbContext.Users
            .Where(u => u.Id == new Guid(UserId) && !u.IsDeleted)
            .SingleAsync();

        if (!BCrypt.Net.BCrypt.Verify(accountDeleteDto.Password, user.Password)) return Unauthorized();

        //user.UpdatorId = user.Id;
        user.IsDeleted = true;

        _mainDbContext.Update(user);
        await _mainDbContext.SaveChangesAsync();
        return NoContent();
    }
}
