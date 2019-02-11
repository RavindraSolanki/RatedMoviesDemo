using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RatedMoviesDemo.Api.Extensions;
using RatedMoviesDemo.Repository;
using RatedMoviesDemo.Repository.Entities;

namespace RatedMoviesDemo.Api.Controllers
{
    [Route("api/bestratedmovies")]
    [ApiController]
    public class BestRatedMoviesController : ControllerBase
    {
        private RatedMoviesContext _ratedMoviesContext;

        public BestRatedMoviesController(RatedMoviesContext ratedMoviesContext)
        {
            _ratedMoviesContext = ratedMoviesContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            var top5MoviesRating = (
                from rating in _ratedMoviesContext.UserMovieRatings
                group rating by rating.MovieId into movieGroup
                join movie in _ratedMoviesContext.Movies on movieGroup.Key equals movie.Id
                orderby movieGroup.Average(_ => _.Rating) descending, movie.Title
                select new
                {
                    Id = movieGroup.Key,
                    AverageRating = movieGroup.Average(_ => _.Rating)
                }).Take(5);

            var movies = _ratedMoviesContext
                .Movies
                .Where(_ => top5MoviesRating.Select(top => top.Id).Contains(_.Id));

            foreach(var movie in movies)
            {
                movie.AverageRating = ((decimal)top5MoviesRating
                    .Single(_ => _.Id == movie.Id)
                    .AverageRating).RoundToNearestPoint5();
            }

            return Ok(movies);
        }
    }
}