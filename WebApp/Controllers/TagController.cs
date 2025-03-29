using AutoMapper;
using FA.Application.Dtos.TagDtos;
using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using WebApp.Commons;
using WebApp.Services;

namespace WebApp.Controllers;

public class TagController(IHttpClientFactory httpClientFactory, IMapper mapper) : 
    BaseEntityController<TagIndexDto, TagDetailDto, TagCreateDto, TagUpdateDto>(httpClientFactory, mapper, Constants.Api.Tag)
{

    [Permission(ModuleAction.IndexTag)]
    public override async Task<ActionResult> Index(int page = 1) => await base.Index(page);


    [Permission(ModuleAction.DetailsTag)]
    public override async Task<ActionResult> Details(Guid id) => await base.Details(id);


    [Permission(ModuleAction.CreateTag)]
    public override ActionResult Create() => base.Create();


    [Permission(ModuleAction.CreateTag)]
    public override async Task<ActionResult> HandleCreate([FromForm] TagCreateDto requestDto)
    {
        if (string.IsNullOrWhiteSpace(requestDto.Name))
        {
            TempData[Constants.MessageType.Error] = "Your request is malformed.";
            return View(requestDto);
        }
        return await base.HandleCreate(requestDto);
    }


    [Permission(ModuleAction.UpdateTag)]
    public override async Task<ActionResult> Edit(Guid id) => await base.Edit(id);


    [Permission(ModuleAction.UpdateTag)]
    public override async Task<ActionResult> HandleEdit(Guid id, TagUpdateDto requestDto)
    {
        if (requestDto.Name != null && string.IsNullOrWhiteSpace(requestDto.Name))
        {
            TempData[Constants.MessageType.Error] = "Your request is malformed.";
            return View(requestDto);
        }
        return await base.HandleEdit(id, requestDto);
    }


    [Permission(ModuleAction.DeleteTag)]
    public override async Task<ActionResult> Delete(Guid id) => await base.Delete(id);


    [Permission(ModuleAction.DeleteTag)]
    public override async Task<ActionResult> HandleDelete(Guid id) => await base.HandleDelete(id);
}