using CompanyASP.Middlewares;
using CompanyASP.Models;
using CompanyASP.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IUserManager, UserManager>();

builder.Services.AddDbContextPool<UserDbContext>(options =>
{    
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerUser"));
});

builder.Services.AddDbContext<CompanyDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerData"));    
});


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

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "User",
        pattern: "{area}/{controller=User}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=User}/{action=Index}/{id?}");
});
app.UseMiddleware<KeyMiddleware>();
app.Run();
