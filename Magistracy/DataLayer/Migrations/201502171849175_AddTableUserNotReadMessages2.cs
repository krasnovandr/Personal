namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableUserNotReadMessages2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersNotReadMessages", "UsersId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersNotReadMessages", "NotReadMessageId", "dbo.NotReadMessages");
            DropIndex("dbo.UsersNotReadMessages", new[] { "UsersId" });
            DropIndex("dbo.UsersNotReadMessages", new[] { "NotReadMessageId" });
            DropPrimaryKey("dbo.NotReadMessages");
            CreateTable(
                "dbo.ConversationNotReadMessages",
                c => new
                    {
                        ConversationId = c.String(nullable: false, maxLength: 128),
                        NotReadMessageId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ConversationId, t.NotReadMessageId })
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .ForeignKey("dbo.NotReadMessages", t => t.NotReadMessageId, cascadeDelete: true)
                .Index(t => t.ConversationId)
                .Index(t => t.NotReadMessageId);
            
            AddColumn("dbo.NotReadMessages", "RecordId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.NotReadMessages", "IdUser", c => c.String());
            AddColumn("dbo.NotReadMessages", "MessageId", c => c.String());
            AddPrimaryKey("dbo.NotReadMessages", "RecordId");
            DropColumn("dbo.NotReadMessages", "NotReadMessageId");
            DropColumn("dbo.NotReadMessages", "Id");
            DropColumn("dbo.NotReadMessages", "ConversationId");
            DropTable("dbo.UsersNotReadMessages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersNotReadMessages",
                c => new
                    {
                        UsersId = c.String(nullable: false, maxLength: 128),
                        NotReadMessageId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UsersId, t.NotReadMessageId });
            
            AddColumn("dbo.NotReadMessages", "ConversationId", c => c.String());
            AddColumn("dbo.NotReadMessages", "Id", c => c.String());
            AddColumn("dbo.NotReadMessages", "NotReadMessageId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.ConversationNotReadMessages", "NotReadMessageId", "dbo.NotReadMessages");
            DropForeignKey("dbo.ConversationNotReadMessages", "ConversationId", "dbo.Conversations");
            DropIndex("dbo.ConversationNotReadMessages", new[] { "NotReadMessageId" });
            DropIndex("dbo.ConversationNotReadMessages", new[] { "ConversationId" });
            DropPrimaryKey("dbo.NotReadMessages");
            DropColumn("dbo.NotReadMessages", "MessageId");
            DropColumn("dbo.NotReadMessages", "IdUser");
            DropColumn("dbo.NotReadMessages", "RecordId");
            DropTable("dbo.ConversationNotReadMessages");
            AddPrimaryKey("dbo.NotReadMessages", "NotReadMessageId");
            CreateIndex("dbo.UsersNotReadMessages", "NotReadMessageId");
            CreateIndex("dbo.UsersNotReadMessages", "UsersId");
            AddForeignKey("dbo.UsersNotReadMessages", "NotReadMessageId", "dbo.NotReadMessages", "NotReadMessageId", cascadeDelete: true);
            AddForeignKey("dbo.UsersNotReadMessages", "UsersId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
