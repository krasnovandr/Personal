namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateKnowledge : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KnowledgeSessions", "Node_Id", "dbo.Nodes");
            DropIndex("dbo.KnowledgeSessions", new[] { "Node_Id" });
            CreateTable(
                "dbo.NodeKnowledgeSessions",
                c => new
                    {
                        Node_Id = c.Int(nullable: false),
                        KnowledgeSession_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Node_Id, t.KnowledgeSession_Id })
                .ForeignKey("dbo.Nodes", t => t.Node_Id, cascadeDelete: true)
                .ForeignKey("dbo.KnowledgeSessions", t => t.KnowledgeSession_Id, cascadeDelete: true)
                .Index(t => t.Node_Id)
                .Index(t => t.KnowledgeSession_Id);
            
            AddColumn("dbo.KnowledgeSessions", "CreatorId", c => c.String());
            DropColumn("dbo.KnowledgeSessions", "Node_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KnowledgeSessions", "Node_Id", c => c.Int());
            DropForeignKey("dbo.NodeKnowledgeSessions", "KnowledgeSession_Id", "dbo.KnowledgeSessions");
            DropForeignKey("dbo.NodeKnowledgeSessions", "Node_Id", "dbo.Nodes");
            DropIndex("dbo.NodeKnowledgeSessions", new[] { "KnowledgeSession_Id" });
            DropIndex("dbo.NodeKnowledgeSessions", new[] { "Node_Id" });
            DropColumn("dbo.KnowledgeSessions", "CreatorId");
            DropTable("dbo.NodeKnowledgeSessions");
            CreateIndex("dbo.KnowledgeSessions", "Node_Id");
            AddForeignKey("dbo.KnowledgeSessions", "Node_Id", "dbo.Nodes", "Id");
        }
    }
}
