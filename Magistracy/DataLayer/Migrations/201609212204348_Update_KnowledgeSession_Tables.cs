namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_KnowledgeSession_Tables : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.KnowledgeSessions", "SessionState");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KnowledgeSessions", "SessionState", c => c.Int(nullable: false));
        }
    }
}
