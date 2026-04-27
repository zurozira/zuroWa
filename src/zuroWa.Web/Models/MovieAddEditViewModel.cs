using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace COMP266EyeMaxCinemas.Models
{
    public class MovieAddEditViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Director { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        [DisplayName("Date Released")]
        [DataType(DataType.Date)]
        public DateTime DateReleased { get; set; }

        [DisplayName("Now Playing")]
        public bool InTheaters { get; set; }

        // All Genre, for a drop down list
        public List<SelectListItem> Genres { get; set; }

        public MovieAddEditViewModel()
        {
            Genres = new List<SelectListItem>();
        }
    }
}
