namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassToUserModel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "SongAtThisMoment_SongId", "dbo.Songs");
            DropIndex("dbo.AspNetUsers", new[] { "SongAtThisMoment_SongId" });
            AddColumn("dbo.AspNetUsers", "SongAtThisMoment", c => c.String());
            DropColumn("dbo.AspNetUsers", "SongAtThisMoment_SongId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "SongAtThisMoment_SongId", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUsers", "SongAtThisMoment");
            CreateIndex("dbo.AspNetUsers", "SongAtThisMoment_SongId");
            AddForeignKey("dbo.AspNetUsers", "SongAtThisMoment_SongId", "dbo.Songs", "SongId");
        }
    }
}
