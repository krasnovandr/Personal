namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSuggestionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SessionNodeSuggestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreation = c.DateTime(nullable: false),
                        Level = c.Int(nullable: false),
                        SuggestedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SuggestedBy_Id)
                .Index(t => t.SuggestedBy_Id);
            
            CreateTable(
                "dbo.SessionNodeSuggestionsKnowledgeSessions",
                c => new
                    {
                        SessionNodeSuggestions_Id = c.Int(nullable: false),
                        KnowledgeSession_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SessionNodeSuggestions_Id, t.KnowledgeSession_Id })
                .ForeignKey("dbo.SessionNodeSuggestions", t => t.SessionNodeSuggestions_Id, cascadeDelete: true)
                .ForeignKey("dbo.KnowledgeSessions", t => t.KnowledgeSession_Id, cascadeDelete: true)
                .Index(t => t.SessionNodeSuggestions_Id)
                .Index(t => t.KnowledgeSession_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SessionNodeSuggestions", "SuggestedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SessionNodeSuggestionsKnowledgeSessions", "KnowledgeSession_Id", "dbo.KnowledgeSessions");
            DropForeignKey("dbo.SessionNodeSuggestionsKnowledgeSessions", "SessionNodeSuggestions_Id", "dbo.SessionNodeSuggestions");
            DropIndex("dbo.SessionNodeSuggestionsKnowledgeSessions", new[] { "KnowledgeSession_Id" });
            DropIndex("dbo.SessionNodeSuggestionsKnowledgeSessions", new[] { "SessionNodeSuggestions_Id" });
            DropIndex("dbo.SessionNodeSuggestions", new[] { "SuggestedBy_Id" });
            DropTable("dbo.SessionNodeSuggestionsKnowledgeSessions");
            DropTable("dbo.SessionNodeSuggestions");
        }
    }
}
