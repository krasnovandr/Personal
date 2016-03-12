namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddparentId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionNodeSuggestions", "ParentId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionNodeSuggestions", "ParentId");
        }
    }
}
