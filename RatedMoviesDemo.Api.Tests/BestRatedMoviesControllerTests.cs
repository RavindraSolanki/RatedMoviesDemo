using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;
using RatedMoviesDemo.Api.Tests.Extensions;
using RatedMoviesDemo.Api.Tests.Utilities;
using RatedMoviesDemo.Api.Controllers;
using RatedMoviesDemo.Repository;
using RatedMoviesDemo.Repository.Entities;

namespace RatedMoviesDemo.Api.Tests
{
    public class BestRatedMoviesControllerTests
    {
        private BestRatedMoviesController _bestRatedMoviesController;
        private InMemoryTestRatedMoviesDatabase _testDatabase;

        public BestRatedMoviesControllerTests()
        {
            _testDatabase = new InMemoryTestRatedMoviesDatabase(RandomString.GetString(10));
            _testDatabase.SeedTestData();
            _bestRatedMoviesController = new BestRatedMoviesController(new RatedMoviesContext(_testDatabase.RatedMoviesContextOptions));
        }

        [Fact]
        public void GetReturnsFiveMovies()
        {
            var response = _bestRatedMoviesController.Get();

            var okResponse = response.Result as OkObjectResult;
            var movies = okResponse.Value as IEnumerable<Movie>;

            Assert.Equal(5, movies.Count());
        }

        [Fact]
        public void GetReturnsFiveMoviesWithAverageRatingInDescendingOrder()
        {
            var response = _bestRatedMoviesController.Get();

            var okResponse = response.Result as OkObjectResult;
            var movies = okResponse.Value as IEnumerable<Movie>;

            var ratings = movies.Select(_ => _.AverageRating);
            Assert.True(ratings.IsInDescendingOrEqualsOrder());
        }
    }
}
