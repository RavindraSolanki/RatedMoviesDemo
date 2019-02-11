using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using RatedMoviesDemo.Api.Tests.Extensions;
using RatedMoviesDemo.Api.Tests.Utilities;
using RatedMoviesDemo.Api.Controllers;
using RatedMoviesDemo.Repository;
using RatedMoviesDemo.Repository.Entities;

namespace RatedMoviesDemo.Api.Tests
{
    public class UserRatedMoviesControllerTests
    {
        private UserRatedMoviesController _userRatedMoviesController;
        private InMemoryTestRatedMoviesDatabase _testDatabase;

        public UserRatedMoviesControllerTests()
        {
            _testDatabase = new InMemoryTestRatedMoviesDatabase(RandomString.GetString(10));
            _testDatabase.SeedTestData();
            _userRatedMoviesController = new UserRatedMoviesController(new RatedMoviesContext(_testDatabase.RatedMoviesContextOptions));
        }

        [Fact]
        public void GetReturnsFiveMovies()
        {
            var response = _userRatedMoviesController.Get(1);

            var okResponse = response.Result as OkObjectResult;
            var movies = okResponse.Value as IEnumerable<Movie>;

            Assert.Equal(5, movies.Count());
        }

        [Fact]
        public void GetReturnsFiveMoviesWithAverageRatingInDescendingOrder()
        {
            var response = _userRatedMoviesController.Get(1);

            var okResponse = response.Result as OkObjectResult;
            var movies = okResponse.Value as IEnumerable<Movie>;

            var ratings = movies.Select(_ => _.AverageRating);
            Assert.True(ratings.IsInDescendingOrEqualsOrder());
        }

        [Fact]
        public void PostAddsNewRating()
        {
            var userIdFromTestDatabase = 1;
            var movieWithNoRatingFromUser = 12;

            var response = _userRatedMoviesController.Post(new UserMovieRating
            {
                UserId = userIdFromTestDatabase,
                MovieId = movieWithNoRatingFromUser,
                Rating = 3
            });

            using (var context = new RatedMoviesContext(_testDatabase.RatedMoviesContextOptions))
            {
                var userRating = context.UserMovieRatings
                    .SingleOrDefault(_ => _.UserId == userIdFromTestDatabase && _.MovieId == movieWithNoRatingFromUser);

                Assert.NotNull(userRating);
            }
        }

        [Fact]
        public  void PostOverridesExistingRating()
        {
            var userIdFromTestDatabase = 1;
            var movieWithExistingRatingFromUser = 12;
            var modifiedRating = (uint)5;

            var response = _userRatedMoviesController.Post(new UserMovieRating
            {
                UserId = userIdFromTestDatabase,
                MovieId = movieWithExistingRatingFromUser,
                Rating = modifiedRating
            });

            using (var context = new RatedMoviesContext(_testDatabase.RatedMoviesContextOptions))
            {
                var userRating = context.UserMovieRatings
                    .Single(_ => _.UserId == userIdFromTestDatabase && _.MovieId == movieWithExistingRatingFromUser);

                Assert.Equal(modifiedRating, userRating.Rating);
            }
        }

        [Fact]
        public  void PostWithInvalidRatingReturnsBadRequest()
        {
            var response = _userRatedMoviesController.Post(new UserMovieRating
            {
                UserId = 1,
                MovieId = 1,
                Rating = 7
            });

            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
