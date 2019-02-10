using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Linq;
using Newtonsoft.Json;
using Xunit;
using RatedMoviesDemo.Repository.Entities;
using RatedMoviesDemo.Api.Tests.Extensions;
using RatedMoviesDemo.Api.Tests.Utilities;

namespace RatedMoviesDemo.Api.Tests
{
    public class UserRatedMoviesControllerTests
    {
        private InMemoryTestServer _testServer;

        public UserRatedMoviesControllerTests()
        {
            _testServer = new InMemoryTestServer();
        }

        [Fact]
        public async void GetReturnsFiveMovies()
        {
            var response = await _testServer.Client.GetAsync("api/userratedmovies");

            var jsonContent = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<List<Movie>>(jsonContent);

            Assert.Equal(5, movies.Count);
        }

        [Fact]
        public async void GetReturnsFiveMoviesWithAverageRatingInDescendingOrder()
        {
            var response = await _testServer.Client.GetAsync("api/userratedmovies");

            var jsonContent = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(jsonContent);

            var ratings = movies.Select(_ => _.AverageRating);
            Assert.True(ratings.IsInDescendingOrEqualsOrder());
        }

        [Fact]
        public async void PostAddsNewRating()
        {
            var jsonRating = JsonConvert.SerializeObject(new UserMovieRating
            {
                UserId = 1,
                MovieId = 1,
                Rating = 4
            });
            var ratingContent = new StringContent(jsonRating);
            var response = await _testServer.Client.PostAsync("api/userratedmovies", ratingContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void PostOverridesExistingRating()
        {
            var jsonRating = JsonConvert.SerializeObject(new UserMovieRating
            {
                UserId = 1,
                MovieId = 2,
                Rating = 4
            });
            var ratingContent = new StringContent(jsonRating);
            var response = await _testServer.Client.PostAsync("api/userratedmovies", ratingContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void PostWithInvalidRatingReturnsBadRequest()
        {
            var jsonRating = JsonConvert.SerializeObject(new UserMovieRating
            {
                UserId = 1,
                MovieId = 1,
                Rating = 7
            });
            var ratingContent = new StringContent(jsonRating);
            var response = await _testServer.Client.PostAsync("api/userratedmovies", ratingContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
