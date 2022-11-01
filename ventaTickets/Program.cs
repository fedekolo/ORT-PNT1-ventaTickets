﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<ventaTicketsContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ventaTicketsContext") ?? throw new InvalidOperationException("Connection string 'ventaTicketsContext' not found.")));

builder.Services.AddDbContext<ventaTicketsContext>(options =>
    options.UseSqlite(@"filename=db\ventaTickets.db"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option => {
    option.LoginPath = "/Acceso/Index";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    option.AccessDeniedPath = "/Home/Privacy";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
