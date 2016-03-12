namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLevelVoteRelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LevelVotes", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.LevelVotes", "ApplicationUser_Id");
            AddForeignKey("dbo.LevelVotes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LevelVotes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.LevelVotes", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.LevelVotes", "ApplicationUser_Id");
        }
    }
}
