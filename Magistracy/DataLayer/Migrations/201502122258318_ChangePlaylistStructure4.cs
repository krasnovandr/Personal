namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePlaylistStructure4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlaylistItems", "Playlist_PlaylistId", "dbo.Playlists");
            DropIndex("dbo.PlaylistItems", new[] { "Playlist_PlaylistId" });
            RenameColumn(table: "dbo.PlaylistItems", name: "Playlist_PlaylistId", newName: "PlaylistId");
            AlterColumn("dbo.PlaylistItems", "PlaylistId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.PlaylistItems", "PlaylistId");
            AddForeignKey("dbo.PlaylistItems", "PlaylistId", "dbo.Playlists", "PlaylistId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlaylistItems", "PlaylistId", "dbo.Playlists");
            DropIndex("dbo.PlaylistItems", new[] { "PlaylistId" });
            AlterColumn("dbo.PlaylistItems", "PlaylistId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.PlaylistItems", name: "PlaylistId", newName: "Playlist_PlaylistId");
            CreateIndex("dbo.PlaylistItems", "Playlist_PlaylistId");
            AddForeignKey("dbo.PlaylistItems", "Playlist_PlaylistId", "dbo.Playlists", "PlaylistId");
        }
    }
}
