﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        public byte NumberInStock { get; set; }

        public byte NumberAvailable { get; set; }
    }
}