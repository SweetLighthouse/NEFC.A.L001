using AutoMapper;
using FA.Application.Dtos.CategoryDtos;
using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using WebApp.Commons;
using WebApp.Services;

namespace WebApp.Controllers;

public class CategoryController(IHttpClientFactory httpClientFactory, IMapper mapper) 
    : BaseEntityController<CategoryIndexDto, CategoryDetailDto, CategoryCreateDto, CategoryUpdateDto>(httpClientFactory, mapper, Constants.Api.Category)
{

    [Permission(ModuleAction.IndexCategory)]
    public override async Task<ActionResult> Index(int page = 1) => await base.Index(page);


    [Permission(ModuleAction.DetailsCategory)]
    public override async Task<ActionResult> Details(Guid id) => await base.Details(id);


    [Permission(ModuleAction.CreateCategory)]
    public override ActionResult Create() => base.Create();


    [Permission(ModuleAction.CreateCategory)]
    public override async Task<ActionResult> HandleCreate([FromForm] CategoryCreateDto requestDto)
    {
        if (string.IsNullOrWhiteSpace(requestDto.Name))
        {
            TempData[Constants.MessageType.Error] = "Your request is malformed.";
            return View(requestDto);
        }
        return await base.HandleCreate(requestDto);
    }


    [Permission(ModuleAction.UpdateCategory)]
    public override async Task<ActionResult> Edit(Guid id) => await base.Edit(id);


    [Permission(ModuleAction.UpdateCategory)]
    public override async Task<ActionResult> HandleEdit(Guid id, CategoryUpdateDto requestDto)
    {
        if (requestDto.Name != null && string.IsNullOrWhiteSpace(requestDto.Name))
        {
            TempData[Constants.MessageType.Error] = "Your request is malformed.";
            return View(requestDto);
        }
        return await base.HandleEdit(id, requestDto);
    }


    [Permission(ModuleAction.DeleteCategory)]
    public override async Task<ActionResult> Delete(Guid id) => await base.Delete(id);


    [Permission(ModuleAction.DeleteCategory)]
    public override async Task<ActionResult> HandleDelete(Guid id) => await base.HandleDelete(id);
}
