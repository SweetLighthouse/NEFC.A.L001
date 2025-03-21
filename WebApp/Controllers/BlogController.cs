using AutoMapper;
using FA.Application.Dtos.BaseDtos;
using FA.Application.Dtos.Blogs;
using FA.Application.Dtos.Categories;
using FA.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using WebApp.Commons;
using WebApp.Services;

namespace WebApp.Controllers;

public class BlogController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly AuthorizerService _authorizerService;
    private readonly IMapper _mapper;
    private string? Role => User.FindFirst(ClaimTypes.Role)?.Value;
    private string ForbiddenMessage => "You do not have permission.";
    public BlogController(IHttpClientFactory httpClientFactory, AuthorizerService authorizerService, IMapper mapper)
    {
        _httpClient = httpClientFactory.CreateClient(Constants.BackendClientName);
        _authorizerService = authorizerService;
        _mapper = mapper;
    }

    private async Task<T?> FetchDataAsync<T>(string apiUrl)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<T>(apiUrl);
        }
        catch (HttpRequestException ex)
        {
            TempData["Error"] = ex.StatusCode switch
            {
                System.Net.HttpStatusCode.Forbidden => ForbiddenMessage,
                System.Net.HttpStatusCode.InternalServerError => "Server error.",
                _ => "A network error occurred."
            };
        }
        catch (JsonException)
        {
            TempData["Error"] = "Error when parsing data.";
        }
        catch
        {
            TempData["Error"] = "Unknown error.";
        }
        return default;
    }

    // GET: BlogController
    public async Task<ActionResult> Index()
    {
        if (!_authorizerService.HasPermission(Role, ModuleAction.IndexBlog)) return View("Forbidden");

        PageResultDto<ResponseCategoryDto>? data = await FetchDataAsync<PageResultDto<ResponseCategoryDto>>(Constants.Api.Blog);
        return View(data);
    }


    // GET: BlogController/Register/5
    public async Task<ActionResult> Details(Guid id)
    {
        if (!_authorizerService.HasPermission(Role, ModuleAction.DetailsBlog)) return View("Forbidden");

        ResponseBlogDto? data = await FetchDataAsync<ResponseBlogDto>($"{Constants.Api.Blog}/{id}");
        if (data is null) return View("404");
        return View(data);
    }

    public ActionResult Create(RequestBlogDto? requestBlogDto = null)
    {
        if (!_authorizerService.HasPermission(Role, ModuleAction.CreateBlog)) return View("Forbidden");
        return View(requestBlogDto);
    }

    [HttpPost]
    public async Task<ActionResult> HandleCreate([FromForm] RequestBlogDto requestBlogDto)
    {
        if (!_authorizerService.HasPermission(Role, ModuleAction.CreateBlog)) return View("Forbidden");
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Invalid input.";
            return View(requestBlogDto);
        }

        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(Constants.Api.Blog, requestBlogDto);
        TempData["Success"] = "Add success.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> Edit(Guid id)
    {
        if (!_authorizerService.HasPermission(Role, ModuleAction.UpdateBlog)) return View("Forbidden");
        ResponseBlogDto? responseBlogDto = await FetchDataAsync<ResponseBlogDto>($"{Constants.Api.Blog}/{id}");
        if (responseBlogDto is null) return View("404");
        RequestBlogDto requestBlogDto = _mapper.Map<RequestBlogDto>(responseBlogDto);
        TempData["Id"] = id;
        return View(requestBlogDto);
    }

    [HttpPost]
    public async Task<ActionResult> HandleEdit(Guid id, [FromForm] RequestBlogDto requestBlogDto)
    {
        if (!_authorizerService.HasPermission(Role, ModuleAction.UpdateBlog)) return View("Forbidden");
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Invalid input.";
            return View(requestBlogDto);
        }
        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync($"{Constants.Api.Blog}/{id}", requestBlogDto);
        TempData["Success"] = "Update success.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> Delete(Guid id)
    {
        if (!_authorizerService.HasPermission(Role, ModuleAction.DeleteBlog)) return View("Forbidden");
        ResponseBlogDto? responseBlogDto = await _httpClient.GetFromJsonAsync<ResponseBlogDto>($"{Constants.Api.Blog}/{id}");
        if (responseBlogDto is null) return View("404");
        return View(responseBlogDto);
    }

    // POST: BlogController/RegisterHandler/5
    [HttpPost]
    public async Task<ActionResult> HandleDelete(Guid id)
    {
        if (!_authorizerService.HasPermission(Role, ModuleAction.DeleteBlog)) return View("Forbidden");
        HttpResponseMessage httpResponseMessage = await _httpClient.DeleteAsync($"{Constants.Api.Blog}/{id}");
        if(httpResponseMessage.IsSuccessStatusCode)
        {
            TempData["Success"] = "Delete success.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["Error"] = "Delete failed.";
            return RedirectToAction(nameof(Index));
        }
    }
}
