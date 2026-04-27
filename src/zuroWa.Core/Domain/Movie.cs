using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace COMP266EyeMaxLib.Domain
{
    // Represents a movie entity in the EyeMaxCinemas,
    // including metadata and optional poster information sourced from TMDB.
    public class Movie
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Director { get; set; }
        
        public DateTime DateReleased { get; set; }
        
        public string Description { get; set; }

        public int CategoryId { get; set; }
        
        public bool InTheaters { get; set; }
        
        // New property for Movie object, stores an image path from TMDB
        public string PosterURL { get; set; }

        public Movie() { }

        public Movie(int id, string title, string director, string description, DateTime dateRelease)
        {
            Id = id;
            Title = title;
            Director = director;
            Description = description;
            DateReleased = dateRelease;          
        }

        public Movie(int id, string title, string director, DateTime dateReleased, string description, int genreId, bool inTheaters)
        {
            Id = id;
            Title = title;
            Director = director;
            Description = description;
            DateReleased = dateReleased;
            CategoryId = genreId;
            InTheaters = inTheaters;
        }

        public Movie(string title, string director, DateTime dateReleased, string description, int categoryId, bool inTheaters)
        {
            Title = title;
            Director = director;
            Description = description;
            DateReleased = dateReleased;
            Description = description;
            CategoryId = categoryId;        
        }
    }
}
