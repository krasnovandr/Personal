namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeConversationStructure : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersConversation", "UsersId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersConversation", "ConversationId", "dbo.Conversations");
            DropIndex("dbo.UsersConversation", new[] { "UsersId" });
            DropIndex("dbo.UsersConversation", new[] { "ConversationId" });
            CreateTable(
                "dbo.UserConversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ConversationId = c.String(),
                        AddDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.UsersConversation");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersConversation",
                c => new
                    {
                        UsersId = c.String(nullable: false, maxLength: 128),
                        ConversationId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UsersId, t.ConversationId });
            
            DropTable("dbo.UserConversations");
            CreateIndex("dbo.UsersConversation", "ConversationId");
            CreateIndex("dbo.UsersConversation", "UsersId");
            AddForeignKey("dbo.UsersConversation", "ConversationId", "dbo.Conversations", "ConversationId", cascadeDelete: true);
            AddForeignKey("dbo.UsersConversation", "UsersId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
