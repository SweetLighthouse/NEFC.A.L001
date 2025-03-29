using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.TagDtos;
using FA.Application.Services;
using FA.Domain.Entities;
using FA.Domain.Enumerations;
using FA.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController(MainDbContext context, IMapper mapper) 
    : BaseEntitiesController<Tag, TagCreateDto, TagUpdateDto, TagIndexDto, TagDetailDto>(context, mapper)
{

    [Permission(ModuleAction.IndexTag)]
    public override async Task<ActionResult<PageResultDto<TagIndexDto>>> GetsAsync(int page = 1, int pageSize = 10) 
        => await base.GetsAsync(page, pageSize);


    [Permission(ModuleAction.DetailsTag)]
    public override async Task<ActionResult<TagDetailDto>> GetByIdAsync(Guid id) => await base.GetByIdAsync(id);


    [Permission(ModuleAction.CreateTag)]
    public override async Task<IActionResult> PostAsync([FromBody] TagCreateDto requestDto) => await base.PostAsync(requestDto);


    [Permission(ModuleAction.UpdateTag)]
    public override async Task<IActionResult> PutAsync(Guid id, [FromBody] TagUpdateDto requestDto) => await base.PutAsync(id, requestDto);


    [Permission(ModuleAction.DeleteTag)]
    public override async Task<IActionResult> DeleteAsync(Guid id) => await base.DeleteAsync(id);
}