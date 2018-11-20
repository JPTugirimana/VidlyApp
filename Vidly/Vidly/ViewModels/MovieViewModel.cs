using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ReleaseDate { get; set; }

        [Required]
        public int Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        [Display(Name="Number In Stock:")]
        [Required]
        [Range(1,20)]
        public byte? NumberInStock { get; set; }

        public DateTime GetDate()
        {
            return DateTime.Parse(String.Format("{0}", ReleaseDate));
        }
    }
}