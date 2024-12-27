using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using QLquanCafe.Data;
using QLquanCafe.Models;
using QLquanCafe.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContect>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AccessData")));
builder.Services.AddScoped<ProductServices>();


builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
// Add services to the container.
builder.Services.AddRazorPages();
// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOrAdmin", policy => policy.RequireRole("User", "Admin"));
});

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// Custom middleware to redirect unauthenticated users to the login page
app.Use(async (context, next) =>
{
    var path = context.Request.Path;

    if (!context.User.Identity.IsAuthenticated && !path.StartsWithSegments("/Login") && !path.StartsWithSegments("/Register"))
    {
        context.Response.Redirect("/Login");
        return;
    }
    await next();
});

app.MapRazorPages();

app.Run();
