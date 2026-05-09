using Microsoft.AspNetCore.Mvc;
using zuroWa.Core.Domain.EyeMax;
using zuroWa.Core.Logic;

namespace zuroWa.Web.Controllers;

public class EyeMaxController(EyeMaxTmdbService tmdbService) : Controller
{
    // GET
    public IActionResult Search()
    {
        return View();
    }

    //POST
    [HttpPost]
    public async Task<ActionResult> SearchResult(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return RedirectToAction(nameof(Search));
        }

        try
        {
            List<TmdbMovie> movies = await tmdbService.SearchMoviesAsync(title);
            return View(movies);
        }
        catch (Exception)
        {
            return RedirectToAction(nameof(Search));
        }
    }
}
