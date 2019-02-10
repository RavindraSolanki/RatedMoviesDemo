using System;

namespace RatedMoviesDemo.Repository.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTimeInMinutes { get; set; }
        public decimal AverageRating { get; set; }
    }
}
