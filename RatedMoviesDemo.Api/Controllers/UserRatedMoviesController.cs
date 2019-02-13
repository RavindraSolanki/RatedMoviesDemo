using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RatedMoviesDemo.Api.Extensions;
using RatedMoviesDemo.Repository;
using RatedMoviesDemo.Repository.Entities;


namespace RatedMoviesDemo.Api.Controllers
{
    [Route("api/userratedmovies")]
    [ApiController]
    public class UserRatedMoviesController : ControllerBase
    {
        private RatedMoviesContext _ratedMoviesContext;

        public UserRatedMoviesController(RatedMoviesContext ratedMoviesContext)
        {
            _ratedMoviesContext = ratedMoviesContext;
        }

        [HttpGet]
        [Route("{userId:int}")]
        public ActionResult<IEnumerable<Movie>> Get(int userId)
        {
            var top5UsersMoviesRating = (
                from rating in _ratedMoviesContext.UserMovieRatings
                where rating.UserId == userId
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
                .Where(_ => top5UsersMoviesRating.Select(top => top.Id).Contains(_.Id));

            foreach (var movie in movies)
            {
                movie.AverageRating = ((decimal)top5UsersMoviesRating
                    .Single(_ => _.Id == movie.Id)
                    .AverageRating).RoundToNearestPoint5();
            }

            return Ok(movies);
        }

        [HttpPost]
        [Route("{userId:int}")]
        public ActionResult Post(int UserId, [FromBody] UserMovieRating userMovieRating)
        {
            if (userMovieRating.Rating < 1 || userMovieRating.Rating > 5)
            {
                return BadRequest("rating should be between 1 and 5");
            }

            userMovieRating.UserId = UserId;

            _ratedMoviesContext.UserMovieRatings.Add(userMovieRating);
            _ratedMoviesContext.SaveChanges();

            return Ok();
        }
    }
}