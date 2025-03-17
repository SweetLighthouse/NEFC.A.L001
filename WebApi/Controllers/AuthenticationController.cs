using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FA.Domain.Entities;
using FA.Infrastructure.Context;
using FA.Application.Dtos.Account;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly MainDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthenticationController(MainDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(AccountDto accountDto)
    {
        try
        {
            // find user with given username
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == accountDto.Username);
            
            // not found or wrong password
            if (user == null || !BCrypt.Net.BCrypt.Verify(accountDto.Password, user.Password))
            {
                return Unauthorized();
            }

            return Ok(GenerateToken(user));
        }
        catch (InvalidOperationException)
        {
            // database broken: multiple user with same username
            return StatusCode(500, "Multiple users found.");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(AccountDto accountDto)
    {
        try
        {
            // if username already used
            bool existed = await _context.Users.AnyAsync(u => u.Username == accountDto.Username);
            if (existed)
            {
                return Conflict();
            }

            User user = _mapper.Map<User>(accountDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(GenerateToken(user));
        }
        catch (InvalidOperationException)
        {
            // database broken: multiple user with same username
            return StatusCode(500, "Multiple users found.");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Roles[0].ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            //Issuer = _configuration["JwtSettings:Issuer"],
            //Audience = _configuration["JwtSettings:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
