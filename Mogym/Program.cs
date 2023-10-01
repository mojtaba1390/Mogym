using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Mogym.Application;
using Mogym.Domain.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();


// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("MogymConnection");
builder.Services.AddDbContext<MogymContext>(x => x.UseSqlServer(connectionString));




var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
