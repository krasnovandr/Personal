namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessagesAndConversationTables2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "CreatorId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "CreatorId");
        }
    }
}
