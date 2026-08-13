using Microsoft.AspNetCore.Authentication.Cookies;
using WebMusic.Data;

var builder = WebApplication.CreateBuilder(args);

Db.Configure(builder.Configuration);

// DI: services (ADO.NET)
builder.Services.AddScoped<WebMusic.Services.IAccountService, WebMusic.Services.AccountService>();
builder.Services.AddScoped<WebMusic.Services.ITheLoaiService, WebMusic.Services.TheLoaiService>();
builder.Services.AddScoped<WebMusic.Services.IChuDeService, WebMusic.Services.ChuDeService>();
builder.Services.AddScoped<WebMusic.Services.IAlbumService, WebMusic.Services.AlbumService>();
builder.Services.AddScoped<WebMusic.Services.IBaiHatService, WebMusic.Services.BaiHatService>();
builder.Services.AddScoped<WebMusic.Services.ICaSiService, WebMusic.Services.CaSiService>();
builder.Services.AddScoped<WebMusic.Services.IPlaylistService, WebMusic.Services.PlaylistService>();
builder.Services.AddScoped<WebMusic.Services.ISearchService, WebMusic.Services.SearchService>();

builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Account/Login";
        o.LogoutPath = "/Account/Logout";
        o.AccessDeniedPath = "/Account/AccessDenied";
        o.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        o.SlidingExpiration = true;
        o.Cookie.HttpOnly = true;
    });
builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();