using FA.Application.Dtos.Permissions;
using FA.Domain.Enumerations;
using WebApp.Commons;

namespace WebApp.Services;

public class AuthorizerService
{
    private readonly HttpClient _httpClient;
    private List<PermissionDto> _permissions = [];

    public AuthorizerService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(Constants.BackendClientName);
    }

    public async Task LoadPermissionsAsync()
    {
        _permissions = await _httpClient.GetFromJsonAsync<List<PermissionDto>>(Constants.Api.Permission)
                     ?? throw new NullReferenceException("Permissions API returned null.");
    }

    public bool HasPermission(Role role, ModuleAction moduleAction)
    {
        var permission = _permissions.FirstOrDefault(p => p.ModuleAction == moduleAction.ToString());
        if (permission == null) return false;

        return role switch
        {
            Role.User => permission.CanUser,
            Role.Contributor => permission.CanContributor,
            Role.BlogOwner => permission.CanBlogOwner,
            _ => false
        };
    }

    public bool HasPermission(string? roleRaw, ModuleAction moduleAction)
    {
        Enum.TryParse<Role>(roleRaw, out var role);
        return HasPermission(role, moduleAction);
    }
}

