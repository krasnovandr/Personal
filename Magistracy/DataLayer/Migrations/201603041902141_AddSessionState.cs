namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSessionState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KnowledgeSessions", "SessionState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KnowledgeSessions", "SessionState");
        }
    }
}
