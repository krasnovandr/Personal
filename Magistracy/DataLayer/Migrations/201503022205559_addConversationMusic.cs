namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addConversationMusic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "MusicConversation", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "MusicConversation");
        }
    }
}
