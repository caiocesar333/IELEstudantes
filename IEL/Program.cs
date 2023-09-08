using IEL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IEL.Models.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<EstadoCidadeService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<EstudantesDbContext>()
        .AddDefaultTokenProviders();


var configuration = builder.Configuration;
builder.Services.AddDbContext<EstudantesDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    CreateDefaultUser(userManager).Wait();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();

async Task CreateDefaultUser(UserManager<ApplicationUser> userManager)
{
    var defaultUser = new ApplicationUser { UserName = "caio@1234", Email = "caio@1234" };

    if (await userManager.FindByNameAsync(defaultUser.UserName) is null)
    {
        var result = await userManager.CreateAsync(defaultUser, "1234");

        if (!result.Succeeded)
        {
            // Log the error or throw an exception
        }
    }
}