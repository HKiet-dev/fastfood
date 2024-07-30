using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using FrontEnd.Components;
using FrontEnd.Services.IService;
using FrontEnd.Services;
using FrontEnd.Utility;
using FrontEnd.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

// Đăng ký thư viện
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
StaticDetails.ApiUrl = builder.Configuration["ServiceUrls:BackEndApi"];
builder.Services.AddSingleton<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<CloudinaryServices>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login"; // Địa chỉ để người dùng được chuyển hướng khi không được xác thực
        options.LogoutPath = "/logout"; // Địa chỉ để người dùng được chuyển hướng khi đăng xuất
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
