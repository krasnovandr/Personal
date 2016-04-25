namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLevelVoteTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LevelVotes", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.LevelVotes", "ParentId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LevelVotes", "ParentId");
            DropColumn("dbo.LevelVotes", "Type");
        }
    }
}
