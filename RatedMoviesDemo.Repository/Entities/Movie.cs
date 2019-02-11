using System;
using System.Collections.Generic;

namespace RatedMoviesDemo.Repository.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTimeInMinutes { get; set; }
        public decimal AverageRating { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<UserMovieRating> Ratings { get; set; }
    }
}
