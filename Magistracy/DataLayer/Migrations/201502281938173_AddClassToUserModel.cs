namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassToUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SongAtThisMoment_SongId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "SongAtThisMoment_SongId");
            AddForeignKey("dbo.AspNetUsers", "SongAtThisMoment_SongId", "dbo.Songs", "SongId");
            DropColumn("dbo.AspNetUsers", "SongAtThisMoment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "SongAtThisMoment", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "SongAtThisMoment_SongId", "dbo.Songs");
            DropIndex("dbo.AspNetUsers", new[] { "SongAtThisMoment_SongId" });
            DropColumn("dbo.AspNetUsers", "SongAtThisMoment_SongId");
        }
    }
}
