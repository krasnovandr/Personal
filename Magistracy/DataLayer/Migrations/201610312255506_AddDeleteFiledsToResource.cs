namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeleteFiledsToResource : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NodeResources", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TextMergeSuggestions", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TextMergeSuggestions", "IsApproved");
            DropColumn("dbo.NodeResources", "IsDeleted");
        }
    }
}
