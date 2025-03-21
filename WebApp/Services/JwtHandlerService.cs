using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;
using WebApp.Commons;

namespace WebApp.Services;

public class JwtHandlerService : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public JwtHandlerService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var jwtToken = _httpContextAccessor.HttpContext?.Request.Cookies[Constants.JwtokenName];

        if (!string.IsNullOrEmpty(jwtToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
