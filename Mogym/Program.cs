using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Mogym.Application;
using Mogym.Domain.Context;
using Mogym.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

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


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
