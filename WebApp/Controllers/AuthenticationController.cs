using FA.Application.Dtos.Accounts;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApp.Commons;

namespace WebApp.Controllers;

public class AuthenticationController(IHttpClientFactory httpClientFactory) : Controller
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Constants.BackendClientName);

    public ActionResult Login() => View();

    public ActionResult Register() => View();

    [HttpPost]
    [ActionName(nameof(Login))]
    public async Task<ActionResult> LoginHandlerAsync([FromForm] LoginDto accountDto)
    {
        if(!ModelState.IsValid)
        {
            TempData["Error"] = "Username or password is invalid.";
            return View();
        }

        HttpResponseMessage httpResponseMessage;
        try
        {
            httpResponseMessage = await _httpClient.PostAsJsonAsync(Constants.Api.Login, accountDto);
        }
        catch
        {
            TempData["Error"] = "Unable to create request to server.";
            return View();
        }

        switch (httpResponseMessage.StatusCode)
        {
            case HttpStatusCode.OK:
                string jwtToken = await httpResponseMessage.Content.ReadAsStringAsync();
                Response.Cookies.Append("JWToken", jwtToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
                TempData["Success"] = "Login success.";
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController)[..^10]);
            case HttpStatusCode.NotFound:
                TempData["Error"] = "Wrong username or password.";
                return View();
            default:
                TempData["Error"] = "Unhandled status code.";
                return View();
        }
    }

    [HttpPost]
    [ActionName(nameof(Register))]
    public async Task<ActionResult> RegisterHandlerAsync([FromForm] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Username or password is invalid.";
            return View();
        }
        HttpResponseMessage httpResponseMessage;
        try
        {
            httpResponseMessage = await _httpClient.PostAsJsonAsync(Constants.Api.Register, registerDto);
        }
        catch
        {
            TempData["Error"] = "Unable to create request to server.";
            return View();
        }

        switch (httpResponseMessage.StatusCode)
        {
            case HttpStatusCode.Created:
                TempData["Success"] = "Register success. Please log in.";
                return RedirectToAction(nameof(Login));
            case HttpStatusCode.Conflict:
                TempData["Error"] = "Username already exists.";
                return View();
            default:
                TempData["Error"] = "Unhandled status code.";
                return View();
        }
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete(Constants.JwtokenName);
        TempData["Success"] = "Logout success.";
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController)[..^10]);
    }
}
