using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using zuroWa.Core.Domain;
using zuroWa.Core.Logic.EyeMax;
using zuroWa.Core.Domain.EyeMax;

namespace zuroWa.Web.Controllers;

public class FavoritesController(EyeMaxFavoriteService eyeMaxFavoriteService, UserManager<AppUser> userManager) : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        try
        {
            List<Movie> movies = await eyeMaxFavoriteService.GetAllAsync();

            return View(movies);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(int tmdbId, string title, string posterPath)
    {
        if (string.IsNullOrEmpty(title)) return BadRequest();

        // Reading from the current user logging (avoid POST fake value claim to be another user)
        var userId = userManager.GetUserId(User);
        if (userId == null) return Unauthorized();
        
        var savedBy = User.Identity!.Name;
        
        var movie = new Movie { 
            TmdbId = tmdbId, 
            Title = title, 
            PosterPath = posterPath,
            UserId = userId,
            SavedBy = savedBy
        };

        try
        {
            await eyeMaxFavoriteService.AddAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Remove(int id)
    {
        try
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();
            
            await eyeMaxFavoriteService.RemoveAsync(id, userId);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }
}