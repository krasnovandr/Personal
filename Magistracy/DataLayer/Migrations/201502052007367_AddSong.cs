namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        SongId = c.String(nullable: false, maxLength: 128),
                        Tag = c.String(),
                        Year = c.String(),
                        Album = c.String(),
                        Genre = c.String(),
                        Artist = c.String(),
                        AddDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SongId);
            
            CreateTable(
                "dbo.UsersSongs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SongId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Id, t.SongId })
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.SongId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersSongs", "SongId", "dbo.Songs");
            DropForeignKey("dbo.UsersSongs", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.UsersSongs", new[] { "SongId" });
            DropIndex("dbo.UsersSongs", new[] { "Id" });
            DropTable("dbo.UsersSongs");
            DropTable("dbo.Songs");
        }
    }
}
