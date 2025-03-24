using FA.Application.Dtos.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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

    [HttpGet]
    public async Task<IActionResult> MyAccount()
    {
        AccountDetailDto? accountDetailDto = await _httpClient.GetFromJsonAsync<AccountDetailDto>(Constants.Api.MyAccount);
        return View(accountDetailDto);
    }

    [HttpGet]
    public async Task<ActionResult> UpdateAccount()
    {
        AccountDetailDto? accountDetailDto = await _httpClient.GetFromJsonAsync<AccountDetailDto>(Constants.Api.MyAccount);
        AccountUpdateDto accountUpdateDto = new()
        {
            Username = accountDetailDto?.Username,
            About = accountDetailDto?.About,
            Email = accountDetailDto?.Email
        };
        return View(accountUpdateDto);
    }

    [HttpPost]
    [ActionName(nameof(UpdateAccount))]
    public async Task<ActionResult> UpdateAccountHandler(AccountUpdateDto accountUpdateDto)
    {
        if (!ModelState.IsValid) return View(accountUpdateDto);
        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync(Constants.Api.UpdateAccount, accountUpdateDto);
        if(httpResponseMessage.IsSuccessStatusCode)
        {
            TempData[Constants.MessageType.Success] = "Update account success.";
            return RedirectToAction(nameof(MyAccount));
        }

        TempData[Constants.MessageType.Error] = httpResponseMessage.StatusCode switch
        {
            HttpStatusCode.Unauthorized => "Your session is over. Please re-login.",
            HttpStatusCode.Conflict => "The username you choose is unavailable.",
            _ => "Some thing went wrong."
        };
        return View(accountUpdateDto);
    }


    [HttpGet]
    public ActionResult DeleteAccount() => View();

    [HttpPost]
    [ActionName(nameof(DeleteAccount))]
    public async Task<ActionResult> DeleteAccountHandler(AccountDeleteDto accountDeleteDto)
    {
        if (!ModelState.IsValid)
        {
            TempData[Constants.MessageType.Error] = "Wrong password.";
            return View(accountDeleteDto);
        }

        HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync(Constants.Api.DeleteAccount, accountDeleteDto);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            Response.Cookies.Delete(Constants.JwtokenName);
            TempData[Constants.MessageType.Success] = "Delete account success.";
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController)[..^10]);
        }

        TempData[Constants.MessageType.Error] = httpResponseMessage.StatusCode switch
        {
            HttpStatusCode.Unauthorized => "Your session is over. Please re-login.",
            HttpStatusCode.BadRequest => "Wrong password.",
            _ => "Some thing went wrong."
        };
        return View(accountDeleteDto);
    }
}
