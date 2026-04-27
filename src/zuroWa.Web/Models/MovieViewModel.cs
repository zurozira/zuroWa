using System.ComponentModel.DataAnnotations;

namespace COMP266EyeMaxCinemas.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Director { get; set; }

        [Display(Name ="Released")]
        [DataType(DataType.Date)]
        public DateTime DateReleased { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name ="Category ID")]
        public int CategoryId { get; set; }
        
        public string Genre { get; set; }

        [Display(Name ="In Theaters")]
        public bool InTheaters { get; set; }
        
        public string PosterPath { get; set; }
    }
}