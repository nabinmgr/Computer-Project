using AutoMapper;
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
    public class MovieController : ApiController
    {
        private ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/movie
        [AllowAnonymous]
        public IHttpActionResult GetMovies(string query=null)
        {
            var movieQuery = _context.Movie.
                Include(m => m.Genre).Where(m=>m.NumberAvailable>0);
            //filter records
            if (!String.IsNullOrWhiteSpace(query))
            {
                movieQuery = movieQuery.Where(m=>m.Name.Contains(query));
            }

            var movieDtos = movieQuery.
                ToList().Select(Mapper.Map<Movie,MovieDto>);
            return Ok(movieDtos);
        }

        //GET /api/movie/1
        [AllowAnonymous]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movie.SingleOrDefault(m => m.ID == id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST api/movie
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movie.Add(movie);
            _context.SaveChanges();

            movieDto.ID = movie.ID;
            return Created(new Uri(Request.RequestUri + "/" + movie.ID),movieDto);
        }

        //PUT api/movie/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id,MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _context.Movie.SingleOrDefault(m => m.ID == id);
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();
            return Ok();
        }

        //DELETE api/movie/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movie.SingleOrDefault(m => m.ID == id);
            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movie.Remove(movie);
            _context.SaveChanges();
            return Ok();
        }
    }
}
