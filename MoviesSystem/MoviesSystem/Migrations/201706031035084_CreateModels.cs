namespace MoviesSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Summary = c.String(),
                        Year = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Rate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rates", t => t.Rate_Id)
                .Index(t => t.Rate_Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RateValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreMovies",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Movie_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Descriptions", "Id", "dbo.Movies");
            DropForeignKey("dbo.Movies", "Rate_Id", "dbo.Rates");
            DropForeignKey("dbo.GenreMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.GenreMovies", new[] { "Movie_Id" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_Id" });
            DropIndex("dbo.Movies", new[] { "Rate_Id" });
            DropIndex("dbo.Descriptions", new[] { "Id" });
            DropTable("dbo.GenreMovies");
            DropTable("dbo.Rates");
            DropTable("dbo.Genres");
            DropTable("dbo.Movies");
            DropTable("dbo.Descriptions");
        }
    }
}
