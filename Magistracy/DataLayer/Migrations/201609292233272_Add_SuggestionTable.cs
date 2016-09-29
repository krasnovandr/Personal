namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SuggestionTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.NodeStructureVotes", newName: "NodeStructureSuggestionVotes");
            RenameColumn(table: "dbo.NodeStructureSuggestionVotes", name: "Node_Id", newName: "SessionNode_Id");
            RenameIndex(table: "dbo.NodeStructureSuggestionVotes", name: "IX_Node_Id", newName: "IX_SessionNode_Id");
            CreateTable(
                "dbo.NodeStructureSuggestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        SuggestedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SuggestedBy_Id)
                .Index(t => t.SuggestedBy_Id);
            
            AddColumn("dbo.SessionNodes", "NodeStructureSuggestion_Id", c => c.Int());
            AddColumn("dbo.NodeStructureSuggestionVotes", "Suggestion_Id", c => c.Int());
            CreateIndex("dbo.SessionNodes", "NodeStructureSuggestion_Id");
            CreateIndex("dbo.NodeStructureSuggestionVotes", "Suggestion_Id");
            AddForeignKey("dbo.SessionNodes", "NodeStructureSuggestion_Id", "dbo.NodeStructureSuggestions", "Id");
            AddForeignKey("dbo.NodeStructureSuggestionVotes", "Suggestion_Id", "dbo.NodeStructureSuggestions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NodeStructureSuggestionVotes", "Suggestion_Id", "dbo.NodeStructureSuggestions");
            DropForeignKey("dbo.NodeStructureSuggestions", "SuggestedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SessionNodes", "NodeStructureSuggestion_Id", "dbo.NodeStructureSuggestions");
            DropIndex("dbo.NodeStructureSuggestionVotes", new[] { "Suggestion_Id" });
            DropIndex("dbo.NodeStructureSuggestions", new[] { "SuggestedBy_Id" });
            DropIndex("dbo.SessionNodes", new[] { "NodeStructureSuggestion_Id" });
            DropColumn("dbo.NodeStructureSuggestionVotes", "Suggestion_Id");
            DropColumn("dbo.SessionNodes", "NodeStructureSuggestion_Id");
            DropTable("dbo.NodeStructureSuggestions");
            RenameIndex(table: "dbo.NodeStructureSuggestionVotes", name: "IX_SessionNode_Id", newName: "IX_Node_Id");
            RenameColumn(table: "dbo.NodeStructureSuggestionVotes", name: "SessionNode_Id", newName: "Node_Id");
            RenameTable(name: "dbo.NodeStructureSuggestionVotes", newName: "NodeStructureVotes");
        }
    }
}
