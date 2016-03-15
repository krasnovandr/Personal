namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSuggestionCommentTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SessionNodeSuggestionsSuggestions", newName: "SuggestionSessionNodeSuggestions");
            RenameTable(name: "dbo.KnowledgeSessionSessionNodeSuggestions", newName: "SessionNodeSuggestionsKnowledgeSessions");
            RenameTable(name: "dbo.ApplicationUserKnowledgeSessions", newName: "KnowledgeSessionApplicationUsers");
            DropPrimaryKey("dbo.SuggestionSessionNodeSuggestions");
            DropPrimaryKey("dbo.SessionNodeSuggestionsKnowledgeSessions");
            DropPrimaryKey("dbo.KnowledgeSessionApplicationUsers");
            AddColumn("dbo.Comments", "CommentBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Suggestions", "SuggestedBy_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Votes", "VoteBy_Id", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.SuggestionSessionNodeSuggestions", new[] { "Suggestion_Id", "SessionNodeSuggestions_Id" });
            AddPrimaryKey("dbo.SessionNodeSuggestionsKnowledgeSessions", new[] { "SessionNodeSuggestions_Id", "KnowledgeSession_Id" });
            AddPrimaryKey("dbo.KnowledgeSessionApplicationUsers", new[] { "KnowledgeSession_Id", "ApplicationUser_Id" });
            CreateIndex("dbo.Comments", "CommentBy_Id");
            CreateIndex("dbo.Suggestions", "SuggestedBy_Id");
            CreateIndex("dbo.Votes", "VoteBy_Id");
            AddForeignKey("dbo.Suggestions", "SuggestedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Votes", "VoteBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "CommentBy_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Comments", "CommentBy");
            DropColumn("dbo.Suggestions", "SuggestedBy");
            DropColumn("dbo.Votes", "VoteBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Votes", "VoteBy", c => c.String());
            AddColumn("dbo.Suggestions", "SuggestedBy", c => c.String());
            AddColumn("dbo.Comments", "CommentBy", c => c.String());
            DropForeignKey("dbo.Comments", "CommentBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votes", "VoteBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Suggestions", "SuggestedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Votes", new[] { "VoteBy_Id" });
            DropIndex("dbo.Suggestions", new[] { "SuggestedBy_Id" });
            DropIndex("dbo.Comments", new[] { "CommentBy_Id" });
            DropPrimaryKey("dbo.KnowledgeSessionApplicationUsers");
            DropPrimaryKey("dbo.SessionNodeSuggestionsKnowledgeSessions");
            DropPrimaryKey("dbo.SuggestionSessionNodeSuggestions");
            DropColumn("dbo.Votes", "VoteBy_Id");
            DropColumn("dbo.Suggestions", "SuggestedBy_Id");
            DropColumn("dbo.Comments", "CommentBy_Id");
            AddPrimaryKey("dbo.KnowledgeSessionApplicationUsers", new[] { "ApplicationUser_Id", "KnowledgeSession_Id" });
            AddPrimaryKey("dbo.SessionNodeSuggestionsKnowledgeSessions", new[] { "KnowledgeSession_Id", "SessionNodeSuggestions_Id" });
            AddPrimaryKey("dbo.SuggestionSessionNodeSuggestions", new[] { "SessionNodeSuggestions_Id", "Suggestion_Id" });
            RenameTable(name: "dbo.KnowledgeSessionApplicationUsers", newName: "ApplicationUserKnowledgeSessions");
            RenameTable(name: "dbo.SessionNodeSuggestionsKnowledgeSessions", newName: "KnowledgeSessionSessionNodeSuggestions");
            RenameTable(name: "dbo.SuggestionSessionNodeSuggestions", newName: "SessionNodeSuggestionsSuggestions");
        }
    }
}
