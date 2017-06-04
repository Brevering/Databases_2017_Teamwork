using MoviesSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMovieSystem
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
