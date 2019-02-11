using System;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using RatedMoviesDemo.Api.Controllers;
using RatedMoviesDemo.Repository;
using RatedMoviesDemo.Api.Tests.Utilities;

namespace RatedMoviesDemo.Api.Tests
{
    public class MoviesControllerTests
    {
        private MoviesController _moviesController;
        private InMemoryTestRatedMoviesDatabase _testDatabase;

        public MoviesControllerTests()
        {
            _testDatabase = new InMemoryTestRatedMoviesDatabase(RandomString.GetString(10));
            _testDatabase.SeedTestData();
            _moviesController = new MoviesController(new RatedMoviesContext(_testDatabase.RatedMoviesContextOptions));
        }

        [Fact]
        public void GetReturnsBadRequestWhenNoFilter()
        {
            var response = _moviesController.Get();
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Theory]
        [InlineData("alien", null, null)]
        [InlineData(null, 1979, null)]
        [InlineData(null, null, new [] { "drama" })]
        [InlineData(null, null, new[] { "drama", "romance" })]
        [InlineData("alien", 1979, new[] { "sci-fi" })]
        public void GetWithFiltersReturnsMovies(string title, int? year, string[] genres)
        {
            uint? yearUint = new uint?();
            if (year.HasValue)
            {
                yearUint = Convert.ToUInt32(year.Value);
            }
            var response = _moviesController.Get(title, yearUint, genres);
            var okResult = response.Result as OkObjectResult;
            var toto = okResult.Value;
        }

        [Fact]
        public void GetWithFiltersNoMovieFound()
        {
            var response = _moviesController.Get("notfoundmovie");
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }
    }
}
