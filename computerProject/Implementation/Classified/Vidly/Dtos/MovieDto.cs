using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public GenreDto Genre { get; set; }

        public int GenreId { get; set; }

        [Required]
        public DateTime ReleasedDate { get; set; }
        
        public DateTime? DateAdded { get; set; }

        [Range(1, 20)]
        public int Stock { get; set; }
    }
}