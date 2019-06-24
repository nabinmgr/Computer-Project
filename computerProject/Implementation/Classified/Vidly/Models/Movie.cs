using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int ID { get; set; }


        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Genre Genre { get; set; }


        [Display(Name = "Genre")]
        public int GenreId { get; set; }


        [Required]
        [Display(Name="Released Date")]
        public DateTime ReleasedDate { get; set; }


        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; }

        [Range(1,20)]
        public int Stock { get; set; }

        public int NumberAvailable { get; set; }
    }
}