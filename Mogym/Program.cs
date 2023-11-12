using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Mogym.Application;
using Mogym.Domain.Context;
using Mogym.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();


// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("MogymConnection");
builder.Services.AddDbContext<MogymContext>(x => x.UseSqlServer(connectionString));

var connectionStringLog = builder.Configuration.GetConnectionString("MogymLogConnection");
builder.Services.AddDbContext<MogymLogContext>(x => x.UseSqlServer(connectionStringLog));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/Account/Login/";
        options.LoginPath = "/Account/Login"; 

    });

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "profile",
    pattern: "{username}",
    defaults: new { controller = "Profile", action = "ProfileInfo" }
);

app.Run();
