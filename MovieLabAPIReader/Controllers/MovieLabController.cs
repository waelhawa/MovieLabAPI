using Microsoft.AspNetCore.Mvc;
using MovieLabAPIReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLabAPIReader.Controllers
{
    public class MovieLabController : Controller
    {
        private readonly MovieLabDAL _movies = new MovieLabDAL();
        public async Task<IActionResult> Index()
        {
            var movies = await _movies.GetMovies();
            return View(movies);
        }

        public IActionResult MovieForm()
        {
            return View(new Movie());
        }


        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _movies.DeleteMovie(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movies.AddMovie(movie);
                return RedirectToAction("Index");
            }
            return View("MovieForm", movie);
        }

        [HttpGet]
        public async Task<IActionResult> EditMovie(int id)
        {
            var movie = await _movies.GetMovie(id);
            return View("MovieForm", movie);
        }

        [HttpPost]
        public async Task<IActionResult> EditMovie(int id, Movie editedMovie)
        {
            if (ModelState.IsValid)
            {
                await _movies.EditMovie(editedMovie, id);
            }
            return RedirectToAction("Index");
        }
    }
}
