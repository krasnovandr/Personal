namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LevelVoteTableUpdateField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LevelVotes", "SessionId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LevelVotes", "SessionId", c => c.String());
        }
    }
}
