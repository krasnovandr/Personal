namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePlaylistStructure : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlaylistSongs", newName: "SongPlaylists");
            RenameColumn(table: "dbo.SongPlaylists", name: "PlaylistId", newName: "Playlist_PlaylistId");
            RenameColumn(table: "dbo.SongPlaylists", name: "SongId", newName: "Song_SongId");
            RenameIndex(table: "dbo.SongPlaylists", name: "IX_SongId", newName: "IX_Song_SongId");
            RenameIndex(table: "dbo.SongPlaylists", name: "IX_PlaylistId", newName: "IX_Playlist_PlaylistId");
            DropPrimaryKey("dbo.SongPlaylists");
            CreateTable(
                "dbo.PlaylistItems",
                c => new
                    {
                        PlaylistItemId = c.String(nullable: false, maxLength: 128),
                        SongId = c.String(),
                        AddDate = c.DateTime(),
                        Playlist_PlaylistId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PlaylistItemId)
                .ForeignKey("dbo.Playlists", t => t.Playlist_PlaylistId)
                .Index(t => t.Playlist_PlaylistId);
            
            AddColumn("dbo.Songs", "ListenCount", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SongPlaylists", new[] { "Song_SongId", "Playlist_PlaylistId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlaylistItems", "Playlist_PlaylistId", "dbo.Playlists");
            DropIndex("dbo.PlaylistItems", new[] { "Playlist_PlaylistId" });
            DropPrimaryKey("dbo.SongPlaylists");
            DropColumn("dbo.Songs", "ListenCount");
            DropTable("dbo.PlaylistItems");
            AddPrimaryKey("dbo.SongPlaylists", new[] { "PlaylistId", "SongId" });
            RenameIndex(table: "dbo.SongPlaylists", name: "IX_Playlist_PlaylistId", newName: "IX_PlaylistId");
            RenameIndex(table: "dbo.SongPlaylists", name: "IX_Song_SongId", newName: "IX_SongId");
            RenameColumn(table: "dbo.SongPlaylists", name: "Song_SongId", newName: "SongId");
            RenameColumn(table: "dbo.SongPlaylists", name: "Playlist_PlaylistId", newName: "PlaylistId");
            RenameTable(name: "dbo.SongPlaylists", newName: "PlaylistSongs");
        }
    }
}
