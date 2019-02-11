using System;
using System.Collections.Generic;

namespace RatedMoviesDemo.Repository.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserMovieRating> RatedMovies { get; set; }
    }
}
