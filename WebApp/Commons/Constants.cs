using Microsoft.Identity.Client;

namespace WebApp.Commons;

public static class Constants
{
    public const string BackendClientName = "BackendApi";
    public const string JwtokenName = "JWToken";
    public static class Api
    {
        public const string Login = "/api/Authentication/login";
        public const string Register = "/api/Authentication/register";
        public const string Permission = "/api/Permissions";

        public const string Blog = "/api/Blogs";
        public const string Category = "/api/Categorys";
        public const string Comment = "/api/Comments";
        public const string Post = "/api/Posts";
        public const string Tag = "/api/Tags";
        public const string User = "/api/Users";

    }
    
}
