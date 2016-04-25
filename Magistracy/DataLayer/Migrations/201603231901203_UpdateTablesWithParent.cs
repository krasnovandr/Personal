namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTablesWithParent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionNodeSuggestions", "Parent_Id", c => c.Int());
            AddColumn("dbo.LevelVotes", "Parent_Id", c => c.Int());
            CreateIndex("dbo.SessionNodeSuggestions", "Parent_Id");
            CreateIndex("dbo.LevelVotes", "Parent_Id");
            AddForeignKey("dbo.SessionNodeSuggestions", "Parent_Id", "dbo.SessionNodeSuggestions", "Id");
            AddForeignKey("dbo.LevelVotes", "Parent_Id", "dbo.SessionNodeSuggestions", "Id");
            DropColumn("dbo.SessionNodeSuggestions", "ParentId");
            DropColumn("dbo.LevelVotes", "ParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LevelVotes", "ParentId", c => c.Int());
            AddColumn("dbo.SessionNodeSuggestions", "ParentId", c => c.Int());
            DropForeignKey("dbo.LevelVotes", "Parent_Id", "dbo.SessionNodeSuggestions");
            DropForeignKey("dbo.SessionNodeSuggestions", "Parent_Id", "dbo.SessionNodeSuggestions");
            DropIndex("dbo.LevelVotes", new[] { "Parent_Id" });
            DropIndex("dbo.SessionNodeSuggestions", new[] { "Parent_Id" });
            DropColumn("dbo.LevelVotes", "Parent_Id");
            DropColumn("dbo.SessionNodeSuggestions", "Parent_Id");
        }
    }
}
