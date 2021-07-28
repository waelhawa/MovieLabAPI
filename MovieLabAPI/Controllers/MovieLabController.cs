using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLabAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLabAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class MovieLabController : Controller
    {
        private readonly MovieLabContext _context = new MovieLabContext();

        public MovieLabController(MovieLabContext context)
        {
            _context = context;
        }

        #region Create
        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(Movie newMovie)
        {
            if (ModelState.IsValid)
            {
                await _context.Movies.AddAsync(newMovie);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetMovie), new { id = newMovie.Id }, newMovie);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion


        #region Read
        // /api/MovieLab
        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return movie;
            }
        }

        #endregion


        #region Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, Movie updatedMovie)
        {
            if (!ModelState.IsValid || id != updatedMovie.Id)
            {
                return BadRequest();
            }
            else
            {
                var dbMovie = _context.Movies.Find(id);
                dbMovie.Title = updatedMovie.Title;
                dbMovie.Genre = updatedMovie.Genre;
                dbMovie.Runtime = updatedMovie.Runtime;
                

                _context.Entry(dbMovie).State = EntityState.Modified;
                _context.Update(dbMovie);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        #endregion


        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return NoContent();
                //204 HTTP status code -- successful and the API is nto returning any content
            }
        }

        #endregion

    }
}
