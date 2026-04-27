using System;
using System.Collections.Generic;
using System.Text;

namespace COMP266EyeMaxLib.Domain
{
    // Represents a movie genre (category) entity in the EyeMaxCinemas.
    public class Genre
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public Genre() { }

        public Genre(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
