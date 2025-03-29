using AutoMapper;
using FA.Application.Dtos.BlogDtos;
using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using WebApp.Commons;
using WebApp.Services;

namespace WebApp.Controllers;

public class BlogController(IHttpClientFactory httpClientFactory, IMapper mapper) 
    : BaseEntityController<BlogIndexDto, BlogDetailDto, BlogCreateDto, BlogUpdateDto>(httpClientFactory, mapper, Constants.Api.Blog)
{

    [Permission(ModuleAction.IndexBlog)]
    public override async Task<ActionResult> Index(int page = 1) => await base.Index(page);


    [Permission(ModuleAction.DetailsBlog)]
    public override async Task<ActionResult> Details(Guid id) => await base.Details(id);


    [Permission(ModuleAction.CreateBlog)]
    public override ActionResult Create() => base.Create();


    [Permission(ModuleAction.CreateBlog)]
    public override async Task<ActionResult> HandleCreate([FromForm] BlogCreateDto requestDto)
    {
        if(string.IsNullOrWhiteSpace(requestDto.Name))
        {
            TempData[Constants.MessageType.Error] = "Your request is malformed.";
            return View(requestDto);
        }
        return await base.HandleCreate(requestDto);
    }


    [Permission(ModuleAction.UpdateBlog)]
    public override async Task<ActionResult> Edit(Guid id) => await base.Edit(id);


    [Permission(ModuleAction.UpdateBlog)]
    public override async Task<ActionResult> HandleEdit(Guid id, BlogUpdateDto requestDto)
    {
        if (requestDto.Name != null && string.IsNullOrWhiteSpace(requestDto.Name))
        {
            TempData[Constants.MessageType.Error] = "Your request is malformed.";
            return View(requestDto);
        }
        return await base.HandleEdit(id, requestDto);
    }


    [Permission(ModuleAction.DeleteBlog)]
    public override async Task<ActionResult> Delete(Guid id) => await base.Delete(id);


    [Permission(ModuleAction.DeleteBlog)]
    public override async Task<ActionResult> HandleDelete(Guid id) => await base.HandleDelete(id);
}
