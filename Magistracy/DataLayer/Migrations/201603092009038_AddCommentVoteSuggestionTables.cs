namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentVoteSuggestionTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserKnowledgeSessionRoles", newName: "KnowledgeSessionRoleApplicationUsers");
            RenameTable(name: "dbo.KnowledgeSessionApplicationUsers", newName: "ApplicationUserKnowledgeSessions");
            RenameTable(name: "dbo.SessionNodeSuggestionsKnowledgeSessions", newName: "KnowledgeSessionSessionNodeSuggestions");
            DropPrimaryKey("dbo.KnowledgeSessionRoleApplicationUsers");
            DropPrimaryKey("dbo.ApplicationUserKnowledgeSessions");
            DropPrimaryKey("dbo.KnowledgeSessionSessionNodeSuggestions");
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Value = c.String(),
                        CommentBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Suggestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SuggestedBy = c.String(),
                        SuggestionDate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        VoteBy = c.String(),
                        VoteDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SuggestionComments",
                c => new
                    {
                        Suggestion_Id = c.Int(nullable: false),
                        Comment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Suggestion_Id, t.Comment_Id })
                .ForeignKey("dbo.Suggestions", t => t.Suggestion_Id, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.Comment_Id, cascadeDelete: true)
                .Index(t => t.Suggestion_Id)
                .Index(t => t.Comment_Id);
            
            CreateTable(
                "dbo.SessionNodeSuggestionsSuggestions",
                c => new
                    {
                        SessionNodeSuggestions_Id = c.Int(nullable: false),
                        Suggestion_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SessionNodeSuggestions_Id, t.Suggestion_Id })
                .ForeignKey("dbo.SessionNodeSuggestions", t => t.SessionNodeSuggestions_Id, cascadeDelete: true)
                .ForeignKey("dbo.Suggestions", t => t.Suggestion_Id, cascadeDelete: true)
                .Index(t => t.SessionNodeSuggestions_Id)
                .Index(t => t.Suggestion_Id);
            
            CreateTable(
                "dbo.VoteSuggestions",
                c => new
                    {
                        Vote_Id = c.Int(nullable: false),
                        Suggestion_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vote_Id, t.Suggestion_Id })
                .ForeignKey("dbo.Votes", t => t.Vote_Id, cascadeDelete: true)
                .ForeignKey("dbo.Suggestions", t => t.Suggestion_Id, cascadeDelete: true)
                .Index(t => t.Vote_Id)
                .Index(t => t.Suggestion_Id);
            
            AddPrimaryKey("dbo.KnowledgeSessionRoleApplicationUsers", new[] { "KnowledgeSessionRole_Id", "ApplicationUser_Id" });
            AddPrimaryKey("dbo.ApplicationUserKnowledgeSessions", new[] { "ApplicationUser_Id", "KnowledgeSession_Id" });
            AddPrimaryKey("dbo.KnowledgeSessionSessionNodeSuggestions", new[] { "KnowledgeSession_Id", "SessionNodeSuggestions_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoteSuggestions", "Suggestion_Id", "dbo.Suggestions");
            DropForeignKey("dbo.VoteSuggestions", "Vote_Id", "dbo.Votes");
            DropForeignKey("dbo.SessionNodeSuggestionsSuggestions", "Suggestion_Id", "dbo.Suggestions");
            DropForeignKey("dbo.SessionNodeSuggestionsSuggestions", "SessionNodeSuggestions_Id", "dbo.SessionNodeSuggestions");
            DropForeignKey("dbo.SuggestionComments", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.SuggestionComments", "Suggestion_Id", "dbo.Suggestions");
            DropIndex("dbo.VoteSuggestions", new[] { "Suggestion_Id" });
            DropIndex("dbo.VoteSuggestions", new[] { "Vote_Id" });
            DropIndex("dbo.SessionNodeSuggestionsSuggestions", new[] { "Suggestion_Id" });
            DropIndex("dbo.SessionNodeSuggestionsSuggestions", new[] { "SessionNodeSuggestions_Id" });
            DropIndex("dbo.SuggestionComments", new[] { "Comment_Id" });
            DropIndex("dbo.SuggestionComments", new[] { "Suggestion_Id" });
            DropPrimaryKey("dbo.KnowledgeSessionSessionNodeSuggestions");
            DropPrimaryKey("dbo.ApplicationUserKnowledgeSessions");
            DropPrimaryKey("dbo.KnowledgeSessionRoleApplicationUsers");
            DropTable("dbo.VoteSuggestions");
            DropTable("dbo.SessionNodeSuggestionsSuggestions");
            DropTable("dbo.SuggestionComments");
            DropTable("dbo.Votes");
            DropTable("dbo.Suggestions");
            DropTable("dbo.Comments");
            AddPrimaryKey("dbo.KnowledgeSessionSessionNodeSuggestions", new[] { "SessionNodeSuggestions_Id", "KnowledgeSession_Id" });
            AddPrimaryKey("dbo.ApplicationUserKnowledgeSessions", new[] { "KnowledgeSession_Id", "ApplicationUser_Id" });
            AddPrimaryKey("dbo.KnowledgeSessionRoleApplicationUsers", new[] { "ApplicationUser_Id", "KnowledgeSessionRole_Id" });
            RenameTable(name: "dbo.KnowledgeSessionSessionNodeSuggestions", newName: "SessionNodeSuggestionsKnowledgeSessions");
            RenameTable(name: "dbo.ApplicationUserKnowledgeSessions", newName: "KnowledgeSessionApplicationUsers");
            RenameTable(name: "dbo.KnowledgeSessionRoleApplicationUsers", newName: "ApplicationUserKnowledgeSessionRoles");
        }
    }
}
