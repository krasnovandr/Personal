namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        PlaylistId = c.String(nullable: false, maxLength: 128),
                        PlayListName = c.String(),
                        Note = c.String(),
                        AddDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PlaylistId);
            
            CreateTable(
                "dbo.PlaylistSongs",
                c => new
                    {
                        PlaylistId = c.String(nullable: false, maxLength: 128),
                        SongId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PlaylistId, t.SongId })
                .ForeignKey("dbo.Playlists", t => t.PlaylistId, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .Index(t => t.PlaylistId)
                .Index(t => t.SongId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlaylistSongs", "SongId", "dbo.Songs");
            DropForeignKey("dbo.PlaylistSongs", "PlaylistId", "dbo.Playlists");
            DropIndex("dbo.PlaylistSongs", new[] { "SongId" });
            DropIndex("dbo.PlaylistSongs", new[] { "PlaylistId" });
            DropTable("dbo.PlaylistSongs");
            DropTable("dbo.Playlists");
        }
    }
}
