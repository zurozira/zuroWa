using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using zuroWa.Core.Domain.EyeMax;
using zuroWa.Core.Logic;

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
    public async Task<ActionResult> Search(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return View(Search());
        }

        try
        {
            List<TmdbMovie> movies = await tmdbService.SearchMoviesAsync(title);
            return View(movies);
        }
        catch (Exception)
        {
            return View(Search());
        }
    }
}
