using System;

namespace RatedMoviesDemo.Repository.Entities
{
    public class UserMovieRating
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public uint Rating { get; set; }
    }
}