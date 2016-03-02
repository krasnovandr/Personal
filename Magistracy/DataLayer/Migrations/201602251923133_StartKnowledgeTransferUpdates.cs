namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartKnowledgeTransferUpdates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KnowledgeSessionRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KnowledgeSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Theme = c.String(),
                        Node_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.Node_Id)
                .Index(t => t.Node_Id);
            
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreation = c.DateTime(nullable: false),
                        ParentId = c.Int(),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserKnowledgeSessionRoles",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        KnowledgeSessionRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.KnowledgeSessionRole_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.KnowledgeSessionRoles", t => t.KnowledgeSessionRole_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.KnowledgeSessionRole_Id);
            
            CreateTable(
                "dbo.KnowledgeSessionApplicationUsers",
                c => new
                    {
                        KnowledgeSession_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.KnowledgeSession_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.KnowledgeSessions", t => t.KnowledgeSession_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.KnowledgeSession_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KnowledgeSessions", "Node_Id", "dbo.Nodes");
            DropForeignKey("dbo.KnowledgeSessionApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.KnowledgeSessionApplicationUsers", "KnowledgeSession_Id", "dbo.KnowledgeSessions");
            DropForeignKey("dbo.ApplicationUserKnowledgeSessionRoles", "KnowledgeSessionRole_Id", "dbo.KnowledgeSessionRoles");
            DropForeignKey("dbo.ApplicationUserKnowledgeSessionRoles", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.KnowledgeSessionApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.KnowledgeSessionApplicationUsers", new[] { "KnowledgeSession_Id" });
            DropIndex("dbo.ApplicationUserKnowledgeSessionRoles", new[] { "KnowledgeSessionRole_Id" });
            DropIndex("dbo.ApplicationUserKnowledgeSessionRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.KnowledgeSessions", new[] { "Node_Id" });
            DropTable("dbo.KnowledgeSessionApplicationUsers");
            DropTable("dbo.ApplicationUserKnowledgeSessionRoles");
            DropTable("dbo.Nodes");
            DropTable("dbo.KnowledgeSessions");
            DropTable("dbo.KnowledgeSessionRoles");
        }
    }
}
