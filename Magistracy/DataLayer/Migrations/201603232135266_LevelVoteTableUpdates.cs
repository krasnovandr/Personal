namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LevelVoteTableUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SessionNodeSuggestions", "Parent_Id", "dbo.SessionNodeSuggestions");
            DropForeignKey("dbo.LevelVotes", "Parent_Id", "dbo.SessionNodeSuggestions");
            DropIndex("dbo.SessionNodeSuggestions", new[] { "Parent_Id" });
            DropIndex("dbo.LevelVotes", new[] { "Parent_Id" });
            AddColumn("dbo.SessionNodeSuggestions", "ParentId", c => c.Int());
            AddColumn("dbo.LevelVotes", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.LevelVotes", "ParentId", c => c.Int());
            DropColumn("dbo.SessionNodeSuggestions", "Parent_Id");
            DropColumn("dbo.LevelVotes", "Parent_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LevelVotes", "Parent_Id", c => c.Int());
            AddColumn("dbo.SessionNodeSuggestions", "Parent_Id", c => c.Int());
            DropColumn("dbo.LevelVotes", "ParentId");
            DropColumn("dbo.LevelVotes", "Date");
            DropColumn("dbo.SessionNodeSuggestions", "ParentId");
            CreateIndex("dbo.LevelVotes", "Parent_Id");
            CreateIndex("dbo.SessionNodeSuggestions", "Parent_Id");
            AddForeignKey("dbo.LevelVotes", "Parent_Id", "dbo.SessionNodeSuggestions", "Id");
            AddForeignKey("dbo.SessionNodeSuggestions", "Parent_Id", "dbo.SessionNodeSuggestions", "Id");
        }
    }
}
