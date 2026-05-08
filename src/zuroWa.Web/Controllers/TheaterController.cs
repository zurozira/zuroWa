using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using COMP266EyeMaxLib.Logic;
using COMP266EyeMaxLib.Domain;
using COMP266EyeMaxCinemas.Models;

namespace COMP266EyeMaxCinemas.Controllers
{
    public class TheaterController : Controller
    {
        private readonly MovieService movieService = new MovieService();
        private readonly GenreService genreService = new GenreService();
        
        // GET: TheaterController
        public async Task<ActionResult> Index()
        {         
            List<Movie> nowPlaying = await movieService.GetNowShowingWithPosters();

            List<MovieViewModel> viewModels = nowPlaying.Select(m => new MovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                DateReleased = m.DateReleased,
                Description = m.Description,
                CategoryId = m.CategoryId,
                InTheaters = m.InTheaters,
                PosterPath = m.PosterURL
            }).ToList();

            return View(viewModels);
        }
        
        // GET: TheaterController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            // COMMENT!!!!!
            Movie m = await movieService.getDetailsWithPoster(id);
            MovieViewModel mModel = new MovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                DateReleased = m.DateReleased,
                Description = m.Description,
                CategoryId = m.CategoryId,
                InTheaters = m.InTheaters,
                PosterPath = m.PosterURL
            };

            Genre genre = genreService.SelectOne(mModel.CategoryId);

            mModel.Genre = genre.Description;
            return View(mModel);
        }

        // GET: TheaterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TheaterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TheaterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TheaterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TheaterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TheaterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
