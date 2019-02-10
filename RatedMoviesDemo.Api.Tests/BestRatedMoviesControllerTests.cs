using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xunit;
using RatedMoviesDemo.Repository.Entities;
using RatedMoviesDemo.Api.Tests.Extensions;
using RatedMoviesDemo.Api.Tests.Utilities;

namespace RatedMoviesDemo.Api.Tests
{
    public class BestRatedMoviesControllerTests
    {
        private InMemoryTestServer _testServer;

        public BestRatedMoviesControllerTests()
        {
            _testServer = new InMemoryTestServer();
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
