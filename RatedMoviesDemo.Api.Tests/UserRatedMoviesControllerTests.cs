using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Text;
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
    public class UserRatedMoviesControllerTests
    {
        private InMemoryTestRatedMoviesDatabase _testDatabase;
        private InMemoryTestServer _testServer;

        public UserRatedMoviesControllerTests()
        {
            var uniquePerTestDatabaseIdentifier = RandomString.GetString(10);
            _testDatabase = new InMemoryTestRatedMoviesDatabase(uniquePerTestDatabaseIdentifier);
            _testDatabase.SeedTestData();

            _testServer = new InMemoryTestServer(_testDatabase.RatedMoviesContextOptions);
        }

        [Fact]
        public async void GetReturnsFiveMovies()
        {
            var response = await _testServer.Client.GetAsync("api/userratedmovies/1");

            var jsonContent = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<List<Movie>>(jsonContent);

            Assert.Equal(5, movies.Count);
        }

        [Fact]
        public async void GetReturnsFiveMoviesWithAverageRatingInDescendingOrder()
        {
            var response = await _testServer.Client.GetAsync("api/userratedmovies/1");

            var jsonContent = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(jsonContent);

            var ratings = movies.Select(_ => _.AverageRating);
            Assert.True(ratings.IsInDescendingOrEqualsOrder());
        }

        [Fact]
        public async void PostAddsNewRating()
        {
            var userIdFromTestDatabase = 1;
            var movieWithNoRatingFromUser = 12;

            var jsonRating = JsonConvert.SerializeObject(new UserMovieRating
            {
                UserId = userIdFromTestDatabase,
                MovieId = movieWithNoRatingFromUser,
                Rating = 3
            });
            var ratingContent = new StringContent(jsonRating, Encoding.UTF8, "application/json");
            var response = await _testServer.Client.PostAsync($"api/userratedmovies/{userIdFromTestDatabase}", ratingContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void PostOverridesExistingRating()
        {
            var userIdFromTestDatabase = 1;
            var movieWithExistingRatingFromUser = 12;
            var modifiedRating = (uint)5;

            var jsonRating = JsonConvert.SerializeObject(new UserMovieRating
            {
                MovieId = movieWithExistingRatingFromUser,
                Rating = modifiedRating
            });

            var ratingContent = new StringContent(jsonRating, Encoding.UTF8, "application/json");
            var response = await _testServer.Client.PostAsync($"api/userratedmovies/{userIdFromTestDatabase}", ratingContent);

            using (var context = new RatedMoviesContext(_testDatabase.RatedMoviesContextOptions))
            {
                var userRating = context.UserMovieRatings
                    .Single(_ => _.UserId == userIdFromTestDatabase && _.MovieId == movieWithExistingRatingFromUser);

                Assert.Equal(modifiedRating, userRating.Rating);
            }
        }

        [Fact]
        public async void PostWithInvalidRatingReturnsBadRequest()
        {
            var jsonRating = JsonConvert.SerializeObject(new UserMovieRating
            {
                MovieId = 1,
                Rating = 7
            });

            var ratingContent = new StringContent(jsonRating, Encoding.UTF8, "application/json");
            var response = await _testServer.Client.PostAsync("api/userratedmovies/1", ratingContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
