using AutoMapper;
using FA.Application.Dtos.Users;
using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using WebApp.Commons;
using WebApp.Services;

namespace WebApp.Controllers;

public class UserController(IHttpClientFactory httpClientFactory, IMapper mapper) 
    : BaseEntityController<UserIndexDto, UserDetailDto, UserCreateDto, UserUpdateDto>(httpClientFactory, mapper, Constants.Api.User)
{

    [Permission(ModuleAction.IndexUser)]
    public override async Task<ActionResult> Index(int page = 1) => await base.Index(page);


    [Permission(ModuleAction.DetailsUser)]
    public override async Task<ActionResult> Details(Guid id) => await base.Details(id);


    [Permission(ModuleAction.CreateUser)]
    public override ActionResult Create() => base.Create();


    [Permission(ModuleAction.CreateUser)]
    public override async Task<ActionResult> HandleCreate([FromForm] UserCreateDto userRequestDto)
    {

        if (string.IsNullOrWhiteSpace(userRequestDto.Username)
            || string.IsNullOrWhiteSpace(userRequestDto.Password)
            || !Enum.IsDefined(userRequestDto.Role))
        {
            TempData[Constants.MessageType.Error] = "Your request is malformed.";
            return View(userRequestDto);
        }

        return await base.HandleCreate(userRequestDto);
    }


    [Permission(ModuleAction.UpdateUser)]
    public override async Task<ActionResult> Edit(Guid id) => await base.Edit(id);


    [Permission(ModuleAction.UpdateUser)]
    public override async Task<ActionResult> HandleEdit(Guid id, UserUpdateDto userRequestDto)
    {
        bool isValid = true;
        if(userRequestDto.Username != null && string.IsNullOrWhiteSpace(userRequestDto.Username)) isValid = false;
        if(userRequestDto.Password != null && string.IsNullOrWhiteSpace(userRequestDto.Password)) isValid = false;
        if(userRequestDto.Role != null && !Enum.IsDefined(userRequestDto.Role.Value)) isValid = false;
        if (!isValid)
        {
            TempData[Constants.MessageType.Error] = "Your request is malformed.";
            return View(userRequestDto);
        }

        return await base.HandleEdit(id, userRequestDto);
    }


    [Permission(ModuleAction.DeleteUser)]
    public override async Task<ActionResult> Delete(Guid id) => await base.Delete(id);


    [Permission(ModuleAction.DeleteUser)]
    public override async Task<ActionResult> HandleDelete(Guid id) => await base.HandleDelete(id);
}
