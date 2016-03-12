namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_LeveVotesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LevelVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Level = c.Int(nullable: false),
                        SessionId = c.String(),
                        SuggetedBy_Id = c.String(maxLength: 128),
                        VoteBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SuggetedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.VoteBy_Id)
                .Index(t => t.SuggetedBy_Id)
                .Index(t => t.VoteBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LevelVotes", "VoteBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LevelVotes", "SuggetedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.LevelVotes", new[] { "VoteBy_Id" });
            DropIndex("dbo.LevelVotes", new[] { "SuggetedBy_Id" });
            DropTable("dbo.LevelVotes");
        }
    }
}
