namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToTextSuggestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TextMergeSuggestions", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.TextMergeSuggestions", "IsApproved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TextMergeSuggestions", "IsApproved", c => c.Boolean(nullable: false));
            DropColumn("dbo.TextMergeSuggestions", "Status");
        }
    }
}
