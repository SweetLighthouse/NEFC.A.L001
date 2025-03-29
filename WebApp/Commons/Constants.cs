using Microsoft.Identity.Client;

namespace WebApp.Commons;

public static class Constants
{
    public const string BackendClientName = "BackendApi";
    public const string JwtokenName = "JWToken";

    public static class MessageType
    {
        public const string Success = "Success";
        public const string Info = "Info";
        public const string Warning = "Warning";
        public const string Error = "Error";
    }

    public static class ViewName
    {
        public const string NotFound = "404";
        public const string Forbidden = "Forbidden";
        public const string InternaServerError = "500";
    }
    public static class Api
    {
        public const string Register = "/api/Authentication/Register";
        public const string Login = "/api/Authentication/Login";
        public const string MyAccount = "/api/Authentication/MyAccount";
        public const string UpdateAccount = "/api/Authentication/UpdateAccount";
        public const string DeleteAccount = "/api/Authentication/DeleteAccount";

        public const string Permission = "/api/Permissions";

        public const string Blog = "/api/Blogs";
        public const string Category = "/api/Categories";
        public const string Comment = "/api/Comments";
        public const string Post = "/api/Posts";
        public const string Tag = "/api/Tags";
        public const string User = "/api/Users";

    }
    
}
