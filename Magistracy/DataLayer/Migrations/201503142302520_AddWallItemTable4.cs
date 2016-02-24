namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWallItemTable4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SongWallItems", "Song_SongId", "dbo.Songs");
            DropForeignKey("dbo.SongWallItems", "WallItem_WallItemId", "dbo.WallItems");
            DropIndex("dbo.SongWallItems", new[] { "Song_SongId" });
            DropIndex("dbo.SongWallItems", new[] { "WallItem_WallItemId" });
            CreateTable(
                "dbo.WallItemsSongs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SongId = c.String(),
                        WallItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.SongWallItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SongWallItems",
                c => new
                    {
                        Song_SongId = c.String(nullable: false, maxLength: 128),
                        WallItem_WallItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_SongId, t.WallItem_WallItemId });
            
            DropTable("dbo.WallItemsSongs");
            CreateIndex("dbo.SongWallItems", "WallItem_WallItemId");
            CreateIndex("dbo.SongWallItems", "Song_SongId");
            AddForeignKey("dbo.SongWallItems", "WallItem_WallItemId", "dbo.WallItems", "WallItemId", cascadeDelete: true);
            AddForeignKey("dbo.SongWallItems", "Song_SongId", "dbo.Songs", "SongId", cascadeDelete: true);
        }
    }
}
