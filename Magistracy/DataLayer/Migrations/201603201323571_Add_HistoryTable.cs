namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_HistoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NodeHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        SessionNodeSuggestionsId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        SuggestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.SessionNodeSuggestions", t => t.SessionNodeSuggestionsId, cascadeDelete: true)
                .ForeignKey("dbo.Suggestions", t => t.SuggestionId, cascadeDelete: true)
                .Index(t => t.SessionNodeSuggestionsId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.SuggestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NodeHistories", "SuggestionId", "dbo.Suggestions");
            DropForeignKey("dbo.NodeHistories", "SessionNodeSuggestionsId", "dbo.SessionNodeSuggestions");
            DropForeignKey("dbo.NodeHistories", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.NodeHistories", new[] { "SuggestionId" });
            DropIndex("dbo.NodeHistories", new[] { "ApplicationUserId" });
            DropIndex("dbo.NodeHistories", new[] { "SessionNodeSuggestionsId" });
            DropTable("dbo.NodeHistories");
        }
    }
}
