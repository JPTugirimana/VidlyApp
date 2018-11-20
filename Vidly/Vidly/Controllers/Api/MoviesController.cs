using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    [Authorize(Roles = RoleName.CanManageMovies)]
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController() 
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        [AllowAnonymous]
        public IEnumerable<MovieDto> GetMovies(string querry = null)
        {
            var moviesQuerry = _context.Movies
                                       .Include(m => m.Genre)
                                       .Where(m => m.NumberAvailable > 0);

            if(!String.IsNullOrWhiteSpace(querry))
            {
                moviesQuerry = moviesQuerry.Where(m => m.Name.Contains(querry));
            }

            var movies = moviesQuerry.ToList();

            return movies.Select(m => new MovieDto() 
            { 
                Id = m.Id,
                Name = m.Name,
                ReleaseDate = m.ReleaseDate,
                GenreId = m.GenreId,
                NumberInStock = m.NumberInStock
            });
        }

        // GET /api/movie/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Name = movie.Name,
                ReleaseDate = movie.ReleaseDate,
                GenreId = movie.GenreId,
                NumberInStock = movie.NumberInStock
            };

            return Ok(movieDto);
        }

        // POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var newMovie = new Movie
            {
                Name = movieDto.Name,
                ReleaseDate = movieDto.ReleaseDate,
                GenreId = movieDto.GenreId,
                NumberInStock = movieDto.NumberInStock
            };

            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            movieDto.Id = newMovie.Id;

            return Created(new Uri(Request.RequestUri + "/" + newMovie.Id), movieDto);
        }

        //PUT /api/movies/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if(movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            movieInDb.Name = movieDto.Name;
            movieInDb.ReleaseDate = movieDto.ReleaseDate;
            movieInDb.GenreId = movieDto.GenreId;
            movieInDb.NumberInStock = movieDto.NumberInStock;

            _context.SaveChanges();
        }

        // DELETE  /api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
