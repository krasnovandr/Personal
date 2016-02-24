namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDialogFieldToConversation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "IsDialog", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "IsDialog");
        }
    }
}
