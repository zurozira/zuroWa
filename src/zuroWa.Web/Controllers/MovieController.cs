using COMP266EyeMaxCinemas.Models;
using COMP266EyeMaxLib.Domain;
using COMP266EyeMaxLib.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace zuroWa.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieService _movieService = new MovieService();
        private readonly GenreService _genreService = new GenreService();
        
        // GET: MovieController
        public async Task<ActionResult> Index()
        {
            List<Movie> movies = await _movieService.SelectAllWithPosters();

            List<MovieViewModel> viewModels = movies.Select(m => new MovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                Description = m.Description,
                DateReleased = m.DateReleased,
                CategoryId = m.CategoryId,
                InTheaters = m.InTheaters,
                PosterPath = m.PosterURL
            }).ToList();

            return View(viewModels);
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        // GET: MovieController/Create
        public ActionResult Create()
        {
            List<Genre> genres = _genreService.SelectAll();

            MovieAddEditViewModel vm = new MovieAddEditViewModel();
            
            vm.DateReleased = DateTime.Now;

            foreach (Genre g in genres)
            {
                vm.Genres.Add(new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Description
                });
            }
            return View(vm);
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieViewModel m)
        {
            try
            {
                _movieService.AddMovie(m.Title, m.Director, m.DateReleased, 
                                        m.Description, m.CategoryId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
                return View(m);
            }
        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieController/Edit/5
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

        // GET: MovieController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Movie m = await _movieService.getDetailsWithPoster(id);
            MovieViewModel mModel = new MovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                PosterPath = m.PosterURL
            };
            return View(mModel);
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _movieService.DeleteMovie(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
                return View(collection);
            }
        }
    }
}
