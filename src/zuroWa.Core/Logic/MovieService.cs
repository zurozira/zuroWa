using System;
using System.Collections.Generic;
using System.Text;
using COMP266EyeMaxLib.Data;
using COMP266EyeMaxLib.Domain;

namespace COMP266EyeMaxLib.Logic
{
    // Access modifier is "public" as this class is called by the presentation tier
    // Provides business logic methods for managing movie data, including
    // retrieval, poster enrichment via TMDB, insertion, and deletion.
    // Acts as the intermediary between the presentation tier and the data tier.
    public class MovieService
    {
        // Call the repository layer and return a list of Movies  
        private MovieRepository movieRepo = new MovieRepository();
        private ImageService imageService = new ImageService();

        // Retrieves all movies from the data source without poster images.
        // returns A List of Movie objects representing all movie records in the database.
        public List<Movie> SelectAll()
        {                
            return movieRepo.SelectAll();
        }

        // Retrieves all movies from the data source and enriches each one
        // with a small poster image URL fetched from the TMDB API.
        // returns A Task resolving to a List of Movie objects, each with its PosterURL property populated.
        public async Task<List<Movie>> SelectAllWithPosters()
        {
            List<Movie> movies = movieRepo.SelectAll();
                             
            // Loop through each movie, and attempt to populate the poster_path using the web service
            foreach (Movie movie in movies)
            {
                // Set the PosterURL prooperty for each movie object by calling ImageService
                movie.PosterURL = await imageService.getPosterImage(movie.Title, "small");
            }
            return movies;
        }

        // Retrieves all movies that are currently showing in theaters without poster images.
        // returns A List of Movie objects where InTheaters true
        public List<Movie> GetNowShowing()
        {
            return movieRepo.GetNowShowing();
        }

        // Retrieves all movies currently showing in theaters and enriches
        // each one with a small poster image URL fetched from the TMDB API.
        // returns A Task resolving to a List of Movie objects currently in theaters,
        // each with its PosterURL property populated.
        public async Task<List<Movie>> GetNowShowingWithPosters()
        {
            List<Movie> playingMovies = movieRepo.GetNowShowing();

            foreach (Movie movie in playingMovies)
            {
                // Set the PosterURL prooperty for each movie object by calling ImageService
                movie.PosterURL = await imageService.getPosterImage(movie.Title, "small");
            }
            return playingMovies;
        }

        // Adds a new movie record to the database with InTheaters defaulted to false.
        public void AddMovie(string title, string director, DateTime dateReleased, string description, int genreId)
        {
            bool inTheaters = false;
            movieRepo.AddMovie(new Movie(title,
                                        director, dateReleased, description,
                                        genreId, inTheaters));
        }

        // Retrieves the full details of a specific movie by its ID, without a poster image.
        public Movie getDetails(int id)
        {
            return movieRepo.SelectOne(id);
        }

        // Retrieves the full details of a specific movie by its ID and add it
        // with a large poster image URL fetched from the TMDB API.
        public async Task<Movie> getDetailsWithPoster(int id)
        {
            Movie movie = movieRepo.SelectOne(id);
            movie.PosterURL = await imageService.getPosterImage(movie.Title, "large");
            return movie;
        }

        // Deletes the movie record with the specified ID from the database.
        public void DeleteMovie(int id)
        {
            movieRepo.DeleteMovie(id);
        }
    }
}
