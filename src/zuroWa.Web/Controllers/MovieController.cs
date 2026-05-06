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


        // Retrieves all movies with their posters and maps them to view models for display.
        // returns A view containing a list of MovieViewModel
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

        // Displays the form to create a new movie. Populates the genre dropdown
        // and sets the default release date to the current date.
        // returns A view with a MovieAddEditViewModel pre-populated with genres
        // GET: MovieController/Create
        public ActionResult Create()
        {
            List<Genre> genres = _genreService.SelectAll();

            MovieAddEditViewModel vm = new MovieAddEditViewModel();

            // I added this line of code since we created a new MovieAddEditViewModel instance, 
            // its DateRealeased will be default (01/01/0001) and it would cause some error adding
            // (only from 1976 onwards)
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

        // Handles the submission of the create movie form. Adds the new movie
        // using the provided view model data.
        // param m - The MovieViewModel containing the new movie's data
        //  returnsRedirects to Index on success; returns the form view with an error message on failure.</returns>
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

        // Displays the delete confirmation page for a specific movie,
        // including the movie's title and poster.
        // param id - The unique identifier of the movie to delete
        // returns A view with a MovieViewModel for confirmation
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

        // Handles the confirmed deletion of a specific movie.
        // param id The unique identifier of the movie to delete
        // param collection - The form data from the confirmation page
        // returns Redirects to Index on success, returns the view with an error message on failure
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
