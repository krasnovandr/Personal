namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToSuggestiontable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Suggestions", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suggestions", "Status");
        }
    }
}
