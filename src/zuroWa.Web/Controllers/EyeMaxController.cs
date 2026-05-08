using Microsoft.AspNetCore.Mvc;
using zuroWa.Core.Logic;
using zuroWa.Core.Domain.EyeMax;

namespace zuroWa.Web.Controllers;

public class EyeMaxController : Controller
{
    private EyeMaxTmdbService tmdbService = new EyeMaxTmdbService();
    
    // GET
    public IActionResult Search()
    {
        return View();
    }

    //POST
    public async Task<ActionResult> SearchResult(string title)
    {
        List<TmdbMovie> movies = await tmdbService.SearchMoviesAsync(title);

        return View(movies);
    }
}
