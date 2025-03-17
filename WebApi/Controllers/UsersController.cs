using Microsoft.AspNetCore.Mvc;
using FA.Application.Services;
using FA.Domain.Enumerations;
using System.Security.Claims;
using AutoMapper;
using FA.Application.Dtos.Users;
using FA.Application.Dtos.BaseDtos;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IMapper _mapper;
    private readonly AuthorizerService _authorizerService;

    public UsersController(UserService userService, IMapper mapper, AuthorizerService authorizerService)
    {
        _userService = userService;
        _mapper = mapper;
        _authorizerService = authorizerService;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<PageResultDto<GeneralUserDto>>> GetUsers(int page = 1, int pageSize = 5)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (_authorizerService.HasPermission(userRole, ModuleAction.User.Delete)) return Forbid();

        PageResultDto<GeneralUserDto> pageResultDto = await _userService.GetUsers(page, pageSize);
        return Ok(pageResultDto);
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DetailUserDto>> GetUser(Guid id)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (_authorizerService.HasPermission(userRole, ModuleAction.User.Details)) return Forbid();

        DetailUserDto? user = await _userService.GetUserByIdAsync(id);

        if (user == null) return NotFound(); // the part 'user == null' is red-underlined fyi
        return Ok(user);
    }

    // POST: api/Users
    [HttpPost]
    public async Task<IActionResult> PostUser(RequestUserDto requestUserDto)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (_authorizerService.HasPermission(userRole, ModuleAction.User.Create)) return Forbid();

        bool success = await _userService.CreateUserAsync(requestUserDto);
        if (success) return Created();
        else return BadRequest();
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(Guid id, RequestUserDto requestUserDto)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (_authorizerService.HasPermission(userRole, ModuleAction.User.Update)) return Forbid();

        bool success = await _userService.UpdateUserAsync(id, requestUserDto);
        if(success) return NoContent();
        return BadRequest();
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        string? userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (_authorizerService.HasPermission(userRole, ModuleAction.User.Delete)) return Forbid();

        bool success = await _userService.DeleteUserAsync(id);
        if (success) return NoContent();
        else return BadRequest();
    }
}
