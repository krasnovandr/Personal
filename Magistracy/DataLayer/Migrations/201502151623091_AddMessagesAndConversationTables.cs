namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessagesAndConversationTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        ConversationId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        FriendId = c.String(),
                        AddDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ConversationId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.String(nullable: false, maxLength: 128),
                        Text = c.String(),
                        From = c.String(),
                        AddDate = c.DateTime(nullable: false),
                        ConversationId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .Index(t => t.ConversationId);
            
            CreateTable(
                "dbo.UsersConversation",
                c => new
                    {
                        UsersId = c.String(nullable: false, maxLength: 128),
                        ConversationId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UsersId, t.ConversationId })
                .ForeignKey("dbo.AspNetUsers", t => t.UsersId, cascadeDelete: true)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .Index(t => t.UsersId)
                .Index(t => t.ConversationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersConversation", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.UsersConversation", "UsersId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropIndex("dbo.UsersConversation", new[] { "ConversationId" });
            DropIndex("dbo.UsersConversation", new[] { "UsersId" });
            DropIndex("dbo.Messages", new[] { "ConversationId" });
            DropTable("dbo.UsersConversation");
            DropTable("dbo.Messages");
            DropTable("dbo.Conversations");
        }
    }
}
