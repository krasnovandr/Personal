namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePlaylistStructure2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SongPlaylists", "Song_SongId", "dbo.Songs");
            DropForeignKey("dbo.SongPlaylists", "Playlist_PlaylistId", "dbo.Playlists");
            DropIndex("dbo.SongPlaylists", new[] { "Song_SongId" });
            DropIndex("dbo.SongPlaylists", new[] { "Playlist_PlaylistId" });
            AddColumn("dbo.Playlists", "Song_SongId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Playlists", "Song_SongId");
            AddForeignKey("dbo.Playlists", "Song_SongId", "dbo.Songs", "SongId");
            DropTable("dbo.SongPlaylists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SongPlaylists",
                c => new
                    {
                        Song_SongId = c.String(nullable: false, maxLength: 128),
                        Playlist_PlaylistId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Song_SongId, t.Playlist_PlaylistId });
            
            DropForeignKey("dbo.Playlists", "Song_SongId", "dbo.Songs");
            DropIndex("dbo.Playlists", new[] { "Song_SongId" });
            DropColumn("dbo.Playlists", "Song_SongId");
            CreateIndex("dbo.SongPlaylists", "Playlist_PlaylistId");
            CreateIndex("dbo.SongPlaylists", "Song_SongId");
            AddForeignKey("dbo.SongPlaylists", "Playlist_PlaylistId", "dbo.Playlists", "PlaylistId", cascadeDelete: true);
            AddForeignKey("dbo.SongPlaylists", "Song_SongId", "dbo.Songs", "SongId", cascadeDelete: true);
        }
    }
}
