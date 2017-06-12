namespace MoviesSystem.Data.PostgreSQLMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description_Id = c.Int(),
                        Rate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Descriptions", t => t.Description_Id)
                .ForeignKey("public.Rates", t => t.Rate_Id)
                .Index(t => t.Description_Id)
                .Index(t => t.Rate_Id);
            
            CreateTable(
                "public.Descriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Summary = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Rates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RateValue = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.MovieActors",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.Actor_Id })
                .ForeignKey("public.Movies", t => t.Movie_Id, cascadeDelete: true)
                .ForeignKey("public.Actors", t => t.Actor_Id, cascadeDelete: true)
                .Index(t => t.Movie_Id)
                .Index(t => t.Actor_Id);
            
            CreateTable(
                "public.GenreMovies",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Movie_Id })
                .ForeignKey("public.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("public.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Movies", "Rate_Id", "public.Rates");
            DropForeignKey("public.GenreMovies", "Movie_Id", "public.Movies");
            DropForeignKey("public.GenreMovies", "Genre_Id", "public.Genres");
            DropForeignKey("public.Movies", "Description_Id", "public.Descriptions");
            DropForeignKey("public.MovieActors", "Actor_Id", "public.Actors");
            DropForeignKey("public.MovieActors", "Movie_Id", "public.Movies");
            DropIndex("public.GenreMovies", new[] { "Movie_Id" });
            DropIndex("public.GenreMovies", new[] { "Genre_Id" });
            DropIndex("public.MovieActors", new[] { "Actor_Id" });
            DropIndex("public.MovieActors", new[] { "Movie_Id" });
            DropIndex("public.Movies", new[] { "Rate_Id" });
            DropIndex("public.Movies", new[] { "Description_Id" });
            DropTable("public.GenreMovies");
            DropTable("public.MovieActors");
            DropTable("public.Rates");
            DropTable("public.Genres");
            DropTable("public.Descriptions");
            DropTable("public.Movies");
            DropTable("public.Actors");
        }
    }
}
