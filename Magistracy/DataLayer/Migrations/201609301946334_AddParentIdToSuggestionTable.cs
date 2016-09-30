namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParentIdToSuggestionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NodeStructureSuggestions", "ParentId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NodeStructureSuggestions", "ParentId");
        }
    }
}
