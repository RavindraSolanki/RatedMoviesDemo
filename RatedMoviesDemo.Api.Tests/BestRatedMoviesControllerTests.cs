using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Newtonsoft.Json;
using RatedMoviesDemo.Api.Tests.Extensions;
using RatedMoviesDemo.Api.Tests.Utilities;
using RatedMoviesDemo.Api.Controllers;
using RatedMoviesDemo.Repository;
using RatedMoviesDemo.Repository.Entities;

namespace RatedMoviesDemo.Api.Tests
{
    public class BestRatedMoviesControllerTests
    {
        private InMemoryTestRatedMoviesDatabase _testDatabase;
        private InMemoryTestServer _testServer;

        public BestRatedMoviesControllerTests()
        {
            var uniquePerTestDatabaseIdentifier = RandomString.GetString(10);
            _testDatabase = new InMemoryTestRatedMoviesDatabase(uniquePerTestDatabaseIdentifier);
            _testDatabase.SeedTestData();

            _testServer = new InMemoryTestServer(_testDatabase.RatedMoviesContextOptions);
        }

        [Fact]
        public async void GetReturnsFiveMovies()
        {
            var response = await _testServer.Client.GetAsync("api/bestratedmovies");

            var jsonContent = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<List<Movie>>(jsonContent);
            
            Assert.Equal(5, movies.Count);
        }

        [Fact]
        public async void GetReturnsFiveMoviesWithAverageRatingInDescendingOrder()
        {
            var response = await _testServer.Client.GetAsync("api/bestratedmovies");

            var jsonContent = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<List<Movie>>(jsonContent);

            var ratings = movies.Select(_ => _.AverageRating);
            Assert.True(ratings.IsInDescendingOrEqualsOrder());
        }
    }
}
