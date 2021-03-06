using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using RatedMoviesDemo.Api.Controllers;
using RatedMoviesDemo.Repository;
using RatedMoviesDemo.Api.Tests.Utilities;

namespace RatedMoviesDemo.Api.Tests
{
    public class MoviesControllerTests
    {
        private InMemoryTestRatedMoviesDatabase _testDatabase;
        private InMemoryTestServer _testServer;

        public MoviesControllerTests()
        {
            var uniquePerTestDatabaseIdentifier = RandomString.GetString(10);
            _testDatabase = new InMemoryTestRatedMoviesDatabase(uniquePerTestDatabaseIdentifier);
            _testDatabase.SeedTestData();

            _testServer = new InMemoryTestServer(_testDatabase.RatedMoviesContextOptions);
        }

        [Fact]
        public async void GetReturnsBadRequestWhenNoFilter()
        {
            var response = await _testServer.Client.GetAsync("api/movies");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("api/movies?title=alien")]
        [InlineData("api/movies?year=1979")]
        [InlineData("api/movies?genres=drama")]
        [InlineData("api/movies?genres=drama&genres=romance")]
        [InlineData("api/movies?title=alien&year=1979&genres=sci-fi")]
        public async void GetWithFiltersReturnsMovies(string urlWithFilters)
        {
            var response = await _testServer.Client.GetAsync(urlWithFilters);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetWithFiltersNoMovieFound()
        {
            var response = await _testServer.Client.GetAsync("api/movies?title=notfoundmovie");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
