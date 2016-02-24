namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConversationMessages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropIndex("dbo.Messages", new[] { "ConversationId" });
            CreateTable(
                "dbo.ConversationMessages",
                c => new
                    {
                        ConversationId = c.String(nullable: false, maxLength: 128),
                        MessageId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ConversationId, t.MessageId })
                .ForeignKey("dbo.Messages", t => t.ConversationId, cascadeDelete: true)
                .ForeignKey("dbo.Conversations", t => t.MessageId, cascadeDelete: true)
                .Index(t => t.ConversationId)
                .Index(t => t.MessageId);
            
            AlterColumn("dbo.Messages", "ConversationId", c => c.String());
            DropColumn("dbo.Conversations", "FriendId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conversations", "FriendId", c => c.String());
            DropForeignKey("dbo.ConversationMessages", "MessageId", "dbo.Conversations");
            DropForeignKey("dbo.ConversationMessages", "ConversationId", "dbo.Messages");
            DropIndex("dbo.ConversationMessages", new[] { "MessageId" });
            DropIndex("dbo.ConversationMessages", new[] { "ConversationId" });
            AlterColumn("dbo.Messages", "ConversationId", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.ConversationMessages");
            CreateIndex("dbo.Messages", "ConversationId");
            AddForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations", "ConversationId", cascadeDelete: true);
        }
    }
}
