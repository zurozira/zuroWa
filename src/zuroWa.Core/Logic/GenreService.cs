using COMP266EyeMaxLib.Data;
using System;
using System.Collections.Generic;
using System.Text;
using COMP266EyeMaxLib.Domain;

namespace COMP266EyeMaxLib.Logic
{
    // Provides business logic methods for retrieving genre (category) data
    // by delegating to the GenreRepository
    public class GenreService
    {
        private GenreRepository genreRepo = new GenreRepository();

        public List<Genre> SelectAll()
        {
            return genreRepo.SelectAll();
        }

        // COMMENT!!!
        public Genre SelectOne(int id)
        {
            return genreRepo.SelectOne(id);
        }
    }
}
