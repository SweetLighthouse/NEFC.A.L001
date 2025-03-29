using AutoMapper;
using FA.Application.Dtos.BlogDtos;
using FA.Application.Dtos.PostDtos;
using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using WebApp.Commons;
using WebApp.Services;

namespace WebApp.Controllers;

public class PostController(IHttpClientFactory httpClientFactory, IMapper mapper)
        : BaseEntityController<PostIndexDto, PostDetailDto, PostCreateDto, PostUpdateDto>(httpClientFactory, mapper, Constants.Api.Post)
{
    [Permission(ModuleAction.IndexPost)]
    public override async Task<ActionResult> Index(int page = 1) => await base.Index(page);


    [Permission(ModuleAction.DetailsPost)]
    public override async Task<ActionResult> Details(Guid id) => await base.Details(id);


    [Permission(ModuleAction.CreatePost)]
    public override ActionResult Create() => View(); 


    [Permission(ModuleAction.CreatePost)]
    public ActionResult CreateWithBlog([FromQuery(Name = "blog-id")] Guid blogId) 
    {
        return View(nameof(Create), new PostCreateDto
        {
            BlogId = blogId,
        });
    }

    [Permission(ModuleAction.CreatePost)]
    public override async Task<ActionResult> HandleCreate([FromForm] PostCreateDto requestDto)
    {
        if (string.IsNullOrWhiteSpace(requestDto.Title) ||
            string.IsNullOrWhiteSpace(requestDto.Content) ||
            //requestDto.BlogId == null ||
            requestDto.CategoryIds is null)
        {
            TempData[Constants.MessageType.Error] = "Your request is malformed.";
            return View(requestDto);
        }

        return await base.HandleCreate(requestDto);
    }


    [Permission(ModuleAction.UpdatePost)]
    public override async Task<ActionResult> Edit(Guid id) => await base.Edit(id);


    [Permission(ModuleAction.UpdatePost)]
    public override async Task<ActionResult> HandleEdit(Guid id, [FromForm] PostUpdateDto requestDto)
    {
        bool isValid = true;
        if (requestDto.Title != null && string.IsNullOrWhiteSpace(requestDto.Title)) isValid = false;
        if (requestDto.Content != null && string.IsNullOrWhiteSpace(requestDto.Content)) isValid = false;
        if (requestDto.BlogId == null) isValid = false;
        if (requestDto.CategoryIds == null) isValid = false;
        if (requestDto.TagNames == null) isValid = false;
        if (!isValid)
        {
            TempData[Constants.MessageType.Error] = "Your request is malformed.";
            return View(requestDto);
        }
        return await base.HandleEdit(id, requestDto);
    }


    [Permission(ModuleAction.DeletePost)]
    public override async Task<ActionResult> Delete(Guid id) => await base.Delete(id);


    [Permission(ModuleAction.DeletePost)]
    public override async Task<ActionResult> HandleDelete(Guid id) => await base.HandleDelete(id);
}
