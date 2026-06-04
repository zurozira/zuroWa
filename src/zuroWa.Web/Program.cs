using Microsoft.EntityFrameworkCore;
using zuroWa.Core.Data;
using zuroWa.Core.Logic;
using zuroWa.Core.Logic.EyeMax;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// My personal added services
// Need to add scope EyeMaxTmdbService and EyeMaxFavoriteService so Controller in Web can use
builder.Services.AddScoped<EyeMaxTmdbService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<EyeMaxFavoriteService>();

var app = builder.Build();

// This ensure the Movies table gets created on Azure on first run
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();