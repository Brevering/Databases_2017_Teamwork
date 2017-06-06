namespace WpfMovieSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieToDbContext : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MovieGenres", newName: "GenreMovies");
            RenameTable(name: "dbo.ActorMovies", newName: "MovieActors");
            DropPrimaryKey("dbo.GenreMovies");
            DropPrimaryKey("dbo.MovieActors");
            AddPrimaryKey("dbo.GenreMovies", new[] { "Genre_Id", "Movie_Id" });
            AddPrimaryKey("dbo.MovieActors", new[] { "Movie_Id", "Actor_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MovieActors");
            DropPrimaryKey("dbo.GenreMovies");
            AddPrimaryKey("dbo.MovieActors", new[] { "Actor_Id", "Movie_Id" });
            AddPrimaryKey("dbo.GenreMovies", new[] { "Movie_Id", "Genre_Id" });
            RenameTable(name: "dbo.MovieActors", newName: "ActorMovies");
            RenameTable(name: "dbo.GenreMovies", newName: "MovieGenres");
        }
    }
}
