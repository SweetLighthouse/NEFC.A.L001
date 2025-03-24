using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.Users;
using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WebApp.Commons;
using WebApp.Services;

namespace WebApp.Controllers;

public class UserController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly AuthorizerService _authorizerService;
    private readonly IMapper _mapper;
    private string? UserId => User.FindFirst(ClaimTypes.Role)?.Value;

    public UserController(IHttpClientFactory httpClientFactory, AuthorizerService authorizerService, IMapper mapper)
    {
        _httpClient = httpClientFactory.CreateClient(Constants.BackendClientName);
        _authorizerService = authorizerService;
        _mapper = mapper;
    }


    // GET: UserController
    public async Task<ActionResult> Index(int page = 1)
    {
        if (!_authorizerService.HasPermission(UserId, ModuleAction.IndexUser)) return View(Constants.ViewName.Forbidden);
        if (page < 1) return RedirectToAction(nameof(this.Index), new { page = 1 }); // page=-1 will route to page=1
        var userPagedDto = await _httpClient.GetFromJsonAsync<PageResultDto<UserIndexDto>>($"{Constants.Api.User}?page={page}&pageSize=2");
        if (page > userPagedDto!.TotalPage) return RedirectToAction(nameof(this.Index), new { page = userPagedDto.TotalPage });
        return View(userPagedDto);
    }

    // GET: UserController/Details/5
    public async Task<ActionResult> Details(Guid id)
    {
        if (!_authorizerService.HasPermission(UserId, ModuleAction.DetailsUser)) return View(Constants.ViewName.Forbidden);
        UserDetailDto? userDetailDto = await _httpClient.GetFromJsonAsync<UserDetailDto>($"{Constants.Api.User}/{id}");
        return View(userDetailDto);
    }

    // GET: UserController/Create
    public ActionResult Create()
    {
        if (!_authorizerService.HasPermission(UserId, ModuleAction.CreateUser)) return View(Constants.ViewName.Forbidden);
        return View();
    }

    // POST: UserController/Create
    [HttpPost]
    [ActionName(nameof(Create))]
    public async Task<ActionResult> HandleCreate([FromForm] UserRequestDto userRequestDto)
    {
        if (!_authorizerService.HasPermission(UserId, ModuleAction.CreateUser)) return View(Constants.ViewName.Forbidden);
        if (!ModelState.IsValid) return View(userRequestDto);
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(Constants.Api.User, userRequestDto);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            TempData[Constants.MessageType.Success] = "Created.";
            return RedirectToAction(nameof(Index));
        }
        TempData[Constants.MessageType.Error] = httpResponseMessage.StatusCode switch
        {
            HttpStatusCode.Forbidden => "You are not allowed to do this.",
            HttpStatusCode.BadRequest => "Your request is malformed.",
            HttpStatusCode.Conflict => "Username is not available.",
            HttpStatusCode.InternalServerError => "Server is broken.",
            _ => "Unhandled status code: " + httpResponseMessage.StatusCode
        };
        return View(userRequestDto);
    }

    // GET: UserController/Edit/5
    public async Task<ActionResult> Edit(Guid id)
    {
        if (!_authorizerService.HasPermission(UserId, ModuleAction.UpdateUser)) return View(Constants.ViewName.Forbidden);
        UserDetailDto? userDetailDto = await _httpClient.GetFromJsonAsync<UserDetailDto>($"{Constants.Api.User}/{id}");
        if (userDetailDto == null) return View(Constants.ViewName.NotFound);
        var userUpdateDto = _mapper.Map<UserUpdateDto>(userDetailDto);
        TempData["Id"] = userDetailDto.Id;
        return View(userUpdateDto);
    }

    // POST: UserController/Edit/5
    [HttpPost]
    [ActionName(nameof(Edit))]
    public async Task<ActionResult> Edit(Guid id, UserUpdateDto userUpdateDto)
    {
        if (!_authorizerService.HasPermission(UserId, ModuleAction.UpdateUser)) return View(Constants.ViewName.Forbidden);
        if (!ModelState.IsValid) return View(userUpdateDto);
        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync($"{Constants.Api.User}/{id}", userUpdateDto);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            TempData[Constants.MessageType.Success] = "Updated.";
            return RedirectToAction(nameof(Index));
        }
        TempData[Constants.MessageType.Error] = httpResponseMessage.StatusCode switch
        {
            HttpStatusCode.Forbidden => "You are not allowed to do this.",
            HttpStatusCode.BadRequest => "Your request is malformed.",
            HttpStatusCode.Conflict => "Username is not available.",
            HttpStatusCode.InternalServerError => "Server is broken.",
            _ => "Unhandled status code: " + httpResponseMessage.StatusCode
        };
        TempData["Id"] = id;
        return View(userUpdateDto);
    }

    // GET: UserController/Edit/5
    public async Task<ActionResult> ChangePassword(Guid id)
    {
        if (!_authorizerService.HasPermission(UserId, ModuleAction.UpdateUser)) return View(Constants.ViewName.Forbidden);
        UserDetailDto? userDetailDto = await _httpClient.GetFromJsonAsync<UserDetailDto>($"{Constants.Api.User}/{id}");
        if (userDetailDto == null) return View(Constants.ViewName.NotFound);
        var abc = _mapper.Map<UserChangePasswordViewDto>(userDetailDto);
        return View(abc);
    }

    [HttpPost]
    [ActionName(nameof(ChangePassword))]
    public async Task<ActionResult> ChangePassword(Guid id, UserChangePasswordDto userChangePasswordDto)
    {
        if (!_authorizerService.HasPermission(UserId, ModuleAction.UpdateUser)) return View(Constants.ViewName.Forbidden);
        if (!ModelState.IsValid) return View(userChangePasswordDto);
        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync($"{Constants.Api.User}/changepassword/{id}", userChangePasswordDto);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            TempData[Constants.MessageType.Success] = "Updated.";
            return RedirectToAction(nameof(Index));
        }
        TempData[Constants.MessageType.Error] = httpResponseMessage.StatusCode switch
        {
            HttpStatusCode.Forbidden => "You are not allowed to do this.",
            HttpStatusCode.BadRequest => "Your request is malformed.",
            HttpStatusCode.Conflict => "Username is not available.",
            HttpStatusCode.InternalServerError => "Server is broken.",
            _ => "Unhandled status code: " + httpResponseMessage.StatusCode
        };
        TempData["Id"] = id;
        return View(userChangePasswordDto);
    }

    // GET: UserController/Delete/5
    public async Task<ActionResult> Delete(Guid id)
    {
        if (!_authorizerService.HasPermission(UserId, ModuleAction.DeleteUser)) return View(Constants.ViewName.Forbidden);
        UserDetailDto? userDetailDto = await _httpClient.GetFromJsonAsync<UserDetailDto>($"{Constants.Api.User}/{id}");
        if (userDetailDto == null) return View(Constants.ViewName.NotFound);
        return View(userDetailDto);
    }

    // POST: UserController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName(nameof(Delete))]
    public async Task<ActionResult> HandleDelete(Guid id)
    {
        if (!_authorizerService.HasPermission(UserId, ModuleAction.DeleteUser)) return View(Constants.ViewName.Forbidden);

        UserDetailDto? userDetailDto = await _httpClient.GetFromJsonAsync<UserDetailDto>($"{Constants.Api.User}/{id}");
        if (userDetailDto == null) return View(Constants.ViewName.NotFound);
        HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync($"{Constants.Api.User}/{id}");
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            TempData[Constants.MessageType.Success] = "Deleted.";
            return RedirectToAction(nameof(Index));
        }
        TempData[Constants.MessageType.Error] = httpResponseMessage.StatusCode switch
        {
            HttpStatusCode.Forbidden => "You are not allowed to do this.",
            HttpStatusCode.BadRequest => "Your request is malformed.",
            HttpStatusCode.Conflict => "Username is not available.",
            HttpStatusCode.InternalServerError => "Server is broken.",
            _ => "Unhandled status code: " + httpResponseMessage.StatusCode
        };
        return View(userDetailDto);
    }
}
