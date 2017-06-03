namespace MoviesSystem.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddActorAndRestrictionsToAllModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Descriptions", "Summary", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Descriptions", "Summary", c => c.String());
        }
    }
}
