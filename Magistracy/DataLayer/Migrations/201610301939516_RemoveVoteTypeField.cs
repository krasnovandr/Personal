namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveVoteTypeField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TextMergeSuggestionVotes", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TextMergeSuggestionVotes", "Type", c => c.Int(nullable: false));
        }
    }
}
