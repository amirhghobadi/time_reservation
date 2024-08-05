using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using time_reservation.Data;
using time_reservation.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region DB Context Connect to Sql server
builder.Services.AddDbContext<TimeReservationContext>(options =>
{
    options.UseSqlServer("Data Source =DESKTOP-AVK98P9;Initial Catalog=TimeReservationDB;Integrated Security=true;Trust Server Certificate=true");
});
#endregion


#region IOC
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
#endregion

#region Authentication

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.LogoutPath = "/Account/Logout";
        option.ExpireTimeSpan = TimeSpan.FromDays(10);
    });

#endregion





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


//
app.UseAuthentication();

app.UseAuthorization();
//



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
