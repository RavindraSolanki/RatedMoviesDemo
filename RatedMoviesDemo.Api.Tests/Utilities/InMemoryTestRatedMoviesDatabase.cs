using System;
using Microsoft.EntityFrameworkCore;
using RatedMoviesDemo.Repository;
using RatedMoviesDemo.Repository.Entities;

namespace RatedMoviesDemo.Api.Tests.Utilities
{
    public class InMemoryTestRatedMoviesDatabase
    {
        public DbContextOptions<RatedMoviesContext> RatedMoviesContextOptions { get; }

        public InMemoryTestRatedMoviesDatabase(string databaseName)
        {
            RatedMoviesContextOptions = new DbContextOptionsBuilder<RatedMoviesContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
        }

        public void SeedTestData()
        {
            using (var context = new RatedMoviesContext(RatedMoviesContextOptions))
            {
                context.Genres.Add(new Genre { Id = 1, Name = "horror" });
                context.Genres.Add(new Genre { Id = 2, Name = "romance" });
                context.Genres.Add(new Genre { Id = 3, Name = "sci-fi" });
                context.Genres.Add(new Genre { Id = 4, Name = "drama" });

                context.Movies.Add(new Movie { Id = 1, Title = "alien", YearOfRelease = 1979, RunningTimeInMinutes = 116 });
                context.Movies.Add(new Movie { Id = 2, Title = "alien covenant", YearOfRelease = 2017, RunningTimeInMinutes = 122 });
                context.Movies.Add(new Movie { Id = 3, Title = "la la land", YearOfRelease = 2016, RunningTimeInMinutes = 128 });
                context.Movies.Add(new Movie { Id = 4, Title = "terminator", YearOfRelease = 1984, RunningTimeInMinutes = 107 });
                context.Movies.Add(new Movie { Id = 5, Title = "titanic", YearOfRelease = 1997, RunningTimeInMinutes = 194 });
                context.Movies.Add(new Movie { Id = 6, Title = "the notebook", YearOfRelease = 2004, RunningTimeInMinutes = 123 });
                context.Movies.Add(new Movie { Id = 7, Title = "blade runner", YearOfRelease = 1982, RunningTimeInMinutes = 117 });
                context.Movies.Add(new Movie { Id = 8, Title = "blade runner 2049", YearOfRelease = 2017, RunningTimeInMinutes = 164 });
                context.Movies.Add(new Movie { Id = 9, Title = "the lord of the rings", YearOfRelease = 2001, RunningTimeInMinutes = 178 });
                context.Movies.Add(new Movie { Id = 10, Title = "les cowboys", YearOfRelease = 2015, RunningTimeInMinutes = 105 });
                context.Movies.Add(new Movie { Id = 11, Title = "un prophete", YearOfRelease = 2009, RunningTimeInMinutes = 155 });
                context.Movies.Add(new Movie { Id = 12, Title = "dheepan", YearOfRelease = 2015, RunningTimeInMinutes = 115 });

                context.MovieGenres.Add(new MovieGenre { MovieId = 1, GenreId = 1 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 1, GenreId = 3 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 2, GenreId = 1 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 2, GenreId = 3 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 3, GenreId = 2 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 3, GenreId = 4 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 4, GenreId = 3 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 5, GenreId = 2 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 5, GenreId = 4 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 6, GenreId = 2 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 6, GenreId = 4 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 7, GenreId = 3 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 7, GenreId = 4 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 8, GenreId = 3 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 8, GenreId = 4 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 9, GenreId = 4 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 10, GenreId = 4 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 11, GenreId = 4 });
                context.MovieGenres.Add(new MovieGenre { MovieId = 12, GenreId = 4 });

                context.Users.Add(new User { Id = 1, Name = "Theodora Acevedo" });
                context.Users.Add(new User { Id = 2, Name = "Jude Combs" });
                context.Users.Add(new User { Id = 3, Name = "Kylo Hewitt" });
                context.Users.Add(new User { Id = 4, Name = "Marvin Lynn" });
                context.Users.Add(new User { Id = 5, Name = "Eisa Schwartz" });
                context.Users.Add(new User { Id = 6, Name = "Mindy Hayden" });
                context.Users.Add(new User { Id = 7, Name = "Teejay Howarth" });

                context.UserMovieRatings.Add(new UserMovieRating { UserId = 1, MovieId = 1, Rating = 4 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 1, MovieId = 2, Rating = 2 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 1, MovieId = 3, Rating = 2 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 1, MovieId = 4, Rating = 3 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 1, MovieId = 5, Rating = 5 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 1, MovieId = 8, Rating = 1 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 2, MovieId = 1, Rating = 4 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 2, MovieId = 2, Rating = 3 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 2, MovieId = 3, Rating = 2 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 2, MovieId = 4, Rating = 1 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 2, MovieId = 5, Rating = 5 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 2, MovieId = 8, Rating = 1 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 2, MovieId = 9, Rating = 4 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 3, MovieId = 1, Rating = 3 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 3, MovieId = 2, Rating = 1 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 3, MovieId = 3, Rating = 5 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 4, MovieId = 1, Rating = 4 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 4, MovieId = 2, Rating = 5 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 4, MovieId = 3, Rating = 2 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 4, MovieId = 4, Rating = 3 });
                context.UserMovieRatings.Add(new UserMovieRating { UserId = 4, MovieId = 5, Rating = 3 });

                context.SaveChanges();
            }
        }
    }
}
