namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSuggestedBy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SessionNodeSuggestions", "SuggestedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SessionNodeSuggestions", new[] { "SuggestedBy_Id" });
            AddColumn("dbo.SessionNodeSuggestions", "SuggestedBy", c => c.String());
            DropColumn("dbo.SessionNodeSuggestions", "SuggestedBy_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SessionNodeSuggestions", "SuggestedBy_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.SessionNodeSuggestions", "SuggestedBy");
            CreateIndex("dbo.SessionNodeSuggestions", "SuggestedBy_Id");
            AddForeignKey("dbo.SessionNodeSuggestions", "SuggestedBy_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
