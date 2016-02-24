namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserSongTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersSongs", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersSongs", "SongId", "dbo.Songs");
            DropIndex("dbo.UsersSongs", new[] { "Id" });
            DropIndex("dbo.UsersSongs", new[] { "SongId" });
            DropTable("dbo.UsersSongs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersSongs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SongId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Id, t.SongId });
            
            CreateIndex("dbo.UsersSongs", "SongId");
            CreateIndex("dbo.UsersSongs", "Id");
            AddForeignKey("dbo.UsersSongs", "SongId", "dbo.Songs", "SongId", cascadeDelete: true);
            AddForeignKey("dbo.UsersSongs", "Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
