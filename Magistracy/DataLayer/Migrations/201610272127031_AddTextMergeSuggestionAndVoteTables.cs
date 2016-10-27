namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTextMergeSuggestionAndVoteTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TextMergeSuggestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Date = c.DateTime(nullable: false),
                        FirstResourceId = c.Int(nullable: false),
                        SecondResourceId = c.Int(nullable: false),
                        Cluster_Id = c.Int(),
                        SuggestedBy_Id = c.String(maxLength: 128),
                        TextMergeSuggestion_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResourceClusters", t => t.Cluster_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SuggestedBy_Id)
                .ForeignKey("dbo.TextMergeSuggestions", t => t.TextMergeSuggestion_Id)
                .Index(t => t.Cluster_Id)
                .Index(t => t.SuggestedBy_Id)
                .Index(t => t.TextMergeSuggestion_Id);
            
            CreateTable(
                "dbo.TextMergeSuggestionVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        TextMergeSuggestion_Id = c.Int(),
                        VoteBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TextMergeSuggestions", t => t.TextMergeSuggestion_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.VoteBy_Id)
                .Index(t => t.TextMergeSuggestion_Id)
                .Index(t => t.VoteBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TextMergeSuggestionVotes", "VoteBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TextMergeSuggestionVotes", "TextMergeSuggestion_Id", "dbo.TextMergeSuggestions");
            DropForeignKey("dbo.TextMergeSuggestions", "TextMergeSuggestion_Id", "dbo.TextMergeSuggestions");
            DropForeignKey("dbo.TextMergeSuggestions", "SuggestedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TextMergeSuggestions", "Cluster_Id", "dbo.ResourceClusters");
            DropIndex("dbo.TextMergeSuggestionVotes", new[] { "VoteBy_Id" });
            DropIndex("dbo.TextMergeSuggestionVotes", new[] { "TextMergeSuggestion_Id" });
            DropIndex("dbo.TextMergeSuggestions", new[] { "TextMergeSuggestion_Id" });
            DropIndex("dbo.TextMergeSuggestions", new[] { "SuggestedBy_Id" });
            DropIndex("dbo.TextMergeSuggestions", new[] { "Cluster_Id" });
            DropTable("dbo.TextMergeSuggestionVotes");
            DropTable("dbo.TextMergeSuggestions");
        }
    }
}
