using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RatedMoviesDemo.Repository;
using RatedMoviesDemo.Repository.Entities;

namespace RatedMoviesDemo.Api.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public RatedMoviesContext _ratedMoviesContext { get; set; }

        public MoviesController(RatedMoviesContext ratedMoviesContext)
        {
            _ratedMoviesContext = ratedMoviesContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get([FromQuery] string title = null,[FromQuery] uint? year = null,[FromQuery] string[] genres = null)
        {
            if (string.IsNullOrWhiteSpace(title) && year == null && (genres == null || genres.Length == 0))
            {
                return BadRequest("At least one filter should be provided");
            }

            var foundMovies = _ratedMoviesContext.Movies.Where(_ => 
                (string.IsNullOrWhiteSpace(title) || _.Title.Contains(title))
                &&
                (year.HasValue == false || _.YearOfRelease == year.Value)
                &&
                (genres == null || genres.Length == 0 || _.Genres.Any(g => genres.Contains(g.Genre.Name)))
            );

            if (foundMovies.Any() == false)
            {
                return NotFound("No movie found");
            }

            return Ok(foundMovies.OrderBy(_ => _.Title));
        }
    }
}
