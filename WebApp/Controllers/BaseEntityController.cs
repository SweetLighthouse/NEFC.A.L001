using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApp.Commons;

namespace WebApp.Controllers;

public abstract class BaseEntityController<TIndexDto, TDetailDto, TCreateDto, TUpdateDto> : Controller
    where TDetailDto : BaseEntityDto
{
    protected readonly string _apiPath;
    protected readonly HttpClient _httpClient;
    protected readonly IMapper _mapper;

    protected BaseEntityController(IHttpClientFactory httpClientFactory, IMapper mapper, string apiPath)
    {
        _httpClient = httpClientFactory.CreateClient(Constants.BackendClientName);
        _mapper = mapper;
        _apiPath = apiPath;
    }

    [HttpGet]
    public virtual async Task<ActionResult> Index(int page = 1)
    {
        if (page < 1) return RedirectToAction(nameof(this.Index), new { page = 1 });
        var pagedDto = await _httpClient.GetFromJsonAsync<PageResultDto<TIndexDto>>($"{_apiPath}?page={page}&pageSize=10");
        if (pagedDto!.TotalItem > 0 && page > pagedDto!.TotalPage) return RedirectToAction(nameof(this.Index), new { page = pagedDto.TotalPage });
        return View(pagedDto);
    }

    [HttpGet]
    public virtual async Task<ActionResult> Details(Guid id)
    {
        var detailDto = await _httpClient.GetFromJsonAsync<TDetailDto>($"{_apiPath}/{id}");
        if (detailDto == null) return View(Constants.ViewName.NotFound);
        return View(detailDto);
    }

    [HttpGet]
    public virtual ActionResult Create() => View();

    [HttpPost]
    [ActionName(nameof(Create))]
    public virtual async Task<ActionResult> HandleCreate([FromForm] TCreateDto requestDto)
    {
        if (!ModelState.IsValid) return View(requestDto);
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(_apiPath, requestDto);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            TempData[Constants.MessageType.Success] = "Created.";
            return RedirectToAction(nameof(Index));
        }
        TempData[Constants.MessageType.Error] = httpResponseMessage.StatusCode switch
        {
            HttpStatusCode.Forbidden => "You are not allowed to do this.",
            HttpStatusCode.BadRequest => "Your request is malformed.",
            HttpStatusCode.Conflict => "Some resource is conflicted.",
            HttpStatusCode.InternalServerError => "Server is broken.",
            _ => "Unhandled status code: " + httpResponseMessage.StatusCode
        };
        return View(requestDto);
    }

    [HttpGet]
    public virtual async Task<ActionResult> Edit(Guid id)
    {
        TDetailDto? detailDto = await _httpClient.GetFromJsonAsync<TDetailDto>($"{_apiPath}/{id}");
        if (detailDto == null) return View(Constants.ViewName.NotFound);
        var updateDto = _mapper.Map<TUpdateDto>(detailDto);
        TempData["Id"] = detailDto.Id;
        return View(updateDto);
    }

    [HttpPost]
    [ActionName(nameof(Edit))]
    public virtual async Task<ActionResult> HandleEdit(Guid id, [FromForm] TUpdateDto updateDto)
    {
        if (!ModelState.IsValid) return View(updateDto);
        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync($"{_apiPath}/{id}", updateDto);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            TempData[Constants.MessageType.Success] = "Updated.";
            return RedirectToAction(nameof(Index));
        }
        TempData[Constants.MessageType.Error] = httpResponseMessage.StatusCode switch
        {
            HttpStatusCode.Forbidden => "You are not allowed to do this.",
            HttpStatusCode.BadRequest => "Your request is malformed.",
            HttpStatusCode.NotFound => "The data you're trying to modify doesn't exist.",
            HttpStatusCode.Conflict => "Some resource is conflicted.",
            HttpStatusCode.InternalServerError => "Server is broken.",
            _ => "Unhandled status code: " + httpResponseMessage.StatusCode
        };
        TempData["Id"] = id;
        return View(updateDto);
    }

    [HttpGet]
    public virtual async Task<ActionResult> Delete(Guid id)
    {
        TDetailDto? detailDto = await _httpClient.GetFromJsonAsync<TDetailDto>($"{_apiPath}/{id}");
        if (detailDto == null) return View(Constants.ViewName.NotFound);
        return View(detailDto);
    }

    [HttpPost]
    [ActionName(nameof(Delete))]
    public virtual async Task<ActionResult> HandleDelete(Guid id)
    {
        TDetailDto? userDetailDto = await _httpClient.GetFromJsonAsync<TDetailDto>($"{_apiPath}/{id}");
        if (userDetailDto == null) return View(Constants.ViewName.NotFound);
        HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync($"{_apiPath}/{id}");
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            TempData[Constants.MessageType.Success] = "Deleted.";
            return RedirectToAction(nameof(Index));
        }
        TempData[Constants.MessageType.Error] = httpResponseMessage.StatusCode switch
        {
            HttpStatusCode.Forbidden => "You are not allowed to do this.",
            HttpStatusCode.BadRequest => "Your request is malformed.",
            HttpStatusCode.NotFound => "The data you're trying to delete doesn't exist.",
            HttpStatusCode.InternalServerError => "Server is broken.",
            _ => "Unhandled status code: " + httpResponseMessage.StatusCode
        };
        return View(userDetailDto);
    }
}
