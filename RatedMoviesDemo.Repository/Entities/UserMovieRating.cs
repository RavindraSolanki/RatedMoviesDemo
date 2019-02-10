using System;

namespace RatedMoviesDemo.Repository.Entities
{
    public class UserMovieRating
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public uint Rating { get; set; }
    }
}