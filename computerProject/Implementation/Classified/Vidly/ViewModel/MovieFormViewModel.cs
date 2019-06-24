using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class MovieFormViewModel
    {

        public IEnumerable<Genre> Genre { get; set; }

        public int? ID { get; set; }


        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        [Display(Name = "Genre")]
        [Required]
        public int? GenreId { get; set; }


        [Required]
        [Display(Name = "Released Date")]
        public DateTime? ReleasedDate { get; set; }

        [Range(1, 20)]
        [Required]
        public int? Stock { get; set; }


        public string Title
        {
            get
            {
                return ID != 0 ? "Edit Movie" : "New Movie";
            }
        }

        public MovieFormViewModel()
        {
            ID = 0;
        }
        public MovieFormViewModel(Movie movie)
        {
            ID = movie.ID;
            Name = movie.Name;
            ReleasedDate = movie.ReleasedDate;
            Stock = movie.Stock;
            GenreId = movie.GenreId;
        }
    }
}