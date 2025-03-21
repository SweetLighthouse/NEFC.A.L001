using FA.Application.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApp.Commons;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<AuthorizerService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<JwtHandlerService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


builder.Services
    .AddHttpClient(Constants.BackendClientName, client => client.BaseAddress = new Uri(builder.Configuration["BackendUrl"]!))
    .AddHttpMessageHandler<JwtHandlerService>();

byte[] key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Read JWT from the "JWToken" cookie
                var token = context.Request.Cookies[Constants.JwtokenName];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token; // Set JWT for validation
                }
                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

await app.Services.GetRequiredService<AuthorizerService>().LoadPermissionsAsync(); 

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
