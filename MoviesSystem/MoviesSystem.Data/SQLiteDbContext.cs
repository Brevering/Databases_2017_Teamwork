using MoviesSystem.Data.SQLiteMigrations;
using MoviesSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Data
{
    public class SQLiteDbContext : DbContext
    {
        public SQLiteDbContext() : base("SQLiteConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SQLiteDbContext, SQLiteConfigurations>(true));
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Description> Description { get; set; }

        public DbSet<Actor> Actors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
