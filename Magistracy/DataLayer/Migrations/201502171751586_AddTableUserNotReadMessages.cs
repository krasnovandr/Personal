namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableUserNotReadMessages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotReadMessages",
                c => new
                    {
                        NotReadMessageId = c.String(nullable: false, maxLength: 128),
                        Id = c.String(),
                        ConversationId = c.String(),
                    })
                .PrimaryKey(t => t.NotReadMessageId);
            
            CreateTable(
                "dbo.UsersNotReadMessages",
                c => new
                    {
                        UsersId = c.String(nullable: false, maxLength: 128),
                        NotReadMessageId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UsersId, t.NotReadMessageId })
                .ForeignKey("dbo.AspNetUsers", t => t.UsersId, cascadeDelete: true)
                .ForeignKey("dbo.NotReadMessages", t => t.NotReadMessageId, cascadeDelete: true)
                .Index(t => t.UsersId)
                .Index(t => t.NotReadMessageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersNotReadMessages", "NotReadMessageId", "dbo.NotReadMessages");
            DropForeignKey("dbo.UsersNotReadMessages", "UsersId", "dbo.AspNetUsers");
            DropIndex("dbo.UsersNotReadMessages", new[] { "NotReadMessageId" });
            DropIndex("dbo.UsersNotReadMessages", new[] { "UsersId" });
            DropTable("dbo.UsersNotReadMessages");
            DropTable("dbo.NotReadMessages");
        }
    }
}
