using MoviesSystem.Models;
using System.Data.Entity;

namespace MoviesSystem
{
    public class MoviesSystemDbContext : DbContext
    {
        public MoviesSystemDbContext() : base("MoviesConnection")
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Description> Description { get; set; }
    }
}
