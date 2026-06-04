using Microsoft.AspNetCore.Mvc;
using zuroWa.Core.Logic.EyeMax;
using zuroWa.Core.Domain.EyeMax;

namespace zuroWa.Web.Controllers;

public class FavoritesController(EyeMaxFavoriteService eyeMaxFavoriteService) : Controller
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
    public async Task<IActionResult> Add(int tmdbId, string title, string posterPath)
    {
        if (string.IsNullOrEmpty(title)) return BadRequest();
        
        var movie = new Movie { 
            TmdbId = tmdbId, Title = title, PosterPath = posterPath
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
    public async Task<IActionResult> Remove(int tmdbId)
    {
        try
        {
            await eyeMaxFavoriteService.RemoveAsync(tmdbId);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }
}