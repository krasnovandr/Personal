namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsToConversation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "LastMessageDate", c => c.DateTime());
            AddColumn("dbo.Conversations", "ConversationAvatarFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "ConversationAvatarFilePath");
            DropColumn("dbo.Conversations", "LastMessageDate");
        }
    }
}
