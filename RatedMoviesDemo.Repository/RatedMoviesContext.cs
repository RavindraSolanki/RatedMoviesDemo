using Microsoft.EntityFrameworkCore;
using RatedMoviesDemo.Repository.Entities;

namespace RatedMoviesDemo.Repository
{
    public class RatedMoviesContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<UserMovieRating> UserMovieRatings { get; set; }

        public RatedMoviesContext(DbContextOptions<RatedMoviesContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>()
                .HasKey(_ => new { _.MovieId, _.GenreId });
            modelBuilder.Entity<MovieGenre>()
                .HasOne(_ => _.Movie)
                .WithMany(_ => _.Genres)
                .HasForeignKey(_ => _.MovieId);
            modelBuilder.Entity<MovieGenre>()
                .HasOne(_ => _.Genre)
                .WithMany(_ => _.Movies)
                .HasForeignKey(_ => _.GenreId);

            modelBuilder.Entity<UserMovieRating>()
                .HasKey(_ => new { _.UserId, _.MovieId });            
        }
    }
}
