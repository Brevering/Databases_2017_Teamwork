namespace WpfMovieSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Yeartypetobeint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Descriptions", "Year", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Descriptions", "Year");
        }
    }
}
