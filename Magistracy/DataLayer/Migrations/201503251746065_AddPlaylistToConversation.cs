namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlaylistToConversation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conversations", "Playlist_PlaylistId", "dbo.Playlists");
            DropIndex("dbo.Conversations", new[] { "Playlist_PlaylistId" });
            AddColumn("dbo.Conversations", "PlaylistId", c => c.String());
            DropColumn("dbo.Conversations", "Playlist_PlaylistId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conversations", "Playlist_PlaylistId", c => c.String(maxLength: 128));
            DropColumn("dbo.Conversations", "PlaylistId");
            CreateIndex("dbo.Conversations", "Playlist_PlaylistId");
            AddForeignKey("dbo.Conversations", "Playlist_PlaylistId", "dbo.Playlists", "PlaylistId");
        }
    }
}
