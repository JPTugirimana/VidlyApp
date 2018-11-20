using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Authorize(Roles = RoleName.CanManageMovies)]
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var movieViewModel = new MovieViewModel
            {
                Genres = _context.Genres.ToList(),
            };

            return View(movieViewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            var viewModel = new MovieViewModel
            {
                Name = movie.Name,
                ReleaseDate = movie.ReleaseDate.ToString("d MMM yyyy"),
                Genre = movie.GenreId,
                NumberInStock = movie.NumberInStock,
                Genres = _context.Genres.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(MovieViewModel newViewModel)
        {
            if(!ModelState.IsValid)
            {
                newViewModel.Genres = _context.Genres.ToList();

                return View("Edit", newViewModel);
            }

            var movie = _context.Movies.SingleOrDefault(m => m.Id == newViewModel.Id);

            movie.Name = newViewModel.Name;
            movie.ReleaseDate = newViewModel.GetDate();
            movie.GenreId = newViewModel.Genre;
            movie.NumberInStock = newViewModel.NumberInStock.Value;

            _context.SaveChanges();

            return RedirectToAction("MovieList", "Movies");
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre)
                                             .SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieViewModel newMovie)
        {
            if(!ModelState.IsValid)
            {
                newMovie.Genres = _context.Genres.ToList();
                return View("Create", newMovie);
            }

            var movie = new Movie
            {
                Name = newMovie.Name,
                ReleaseDate = newMovie.GetDate(),
                DateAdded = DateTime.Now,
                GenreId = newMovie.Genre,
                NumberInStock = newMovie.NumberInStock.Value,
                NumberAvailable = newMovie.NumberInStock.Value
            };

            _context.Movies.Add(movie);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult MovieList()
        {
            var movieList = _context.Movies.Include(m => m.Genre).ToList();

            if(User.IsInRole(RoleName.CanManageMovies))
                return View("MovieList", movieList);

            return View("MovieListReadOnly", movieList);
        }
    }
}