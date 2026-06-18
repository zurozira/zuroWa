using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using zuroWa.Core.Data;
using zuroWa.Core.Logic;
using zuroWa.Core.Domain;
using zuroWa.Core.Logic.EyeMax;
using zuroWa.Core.Logic.Ponsai;
using zuroWa.Core.Logic.ZicZacZu;
using zuroWa.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultIdentity<AppUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Override login/logout path
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// My personal added services
// Need to add scope EyeMaxTmdbService and EyeMaxFavoriteService so Controller in Web can use
builder.Services.AddScoped<EyeMaxTmdbService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<EyeMaxFavoriteService>();

// After building PonsaiService, I add it here as well
builder.Services.AddScoped<PonsaiService>();

// ZicZacZu service
builder.Services.AddScoped<ZicZacZuService>();

var app = builder.Build();

// This ensures the Tables gets created on Azure on first run
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    await AppSeeder.SeedAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();