using System;
using System.Collections.Generic;

namespace RatedMoviesDemo.Repository.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MovieGenre> Movies { get; set; }
    }
}
