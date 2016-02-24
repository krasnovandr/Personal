namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeConversationStructureAddPlaylist : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ConversationPlaylists", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.ConversationPlaylists", "PlaylistId", "dbo.Playlists");
            DropIndex("dbo.ConversationPlaylists", new[] { "ConversationId" });
            DropIndex("dbo.ConversationPlaylists", new[] { "PlaylistId" });
            AddColumn("dbo.Conversations", "Playlist_PlaylistId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Conversations", "Playlist_PlaylistId");
            AddForeignKey("dbo.Conversations", "Playlist_PlaylistId", "dbo.Playlists", "PlaylistId");
            DropTable("dbo.ConversationPlaylists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ConversationPlaylists",
                c => new
                    {
                        ConversationId = c.String(nullable: false, maxLength: 128),
                        PlaylistId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ConversationId, t.PlaylistId });
            
            DropForeignKey("dbo.Conversations", "Playlist_PlaylistId", "dbo.Playlists");
            DropIndex("dbo.Conversations", new[] { "Playlist_PlaylistId" });
            DropColumn("dbo.Conversations", "Playlist_PlaylistId");
            CreateIndex("dbo.ConversationPlaylists", "PlaylistId");
            CreateIndex("dbo.ConversationPlaylists", "ConversationId");
            AddForeignKey("dbo.ConversationPlaylists", "PlaylistId", "dbo.Playlists", "PlaylistId", cascadeDelete: true);
            AddForeignKey("dbo.ConversationPlaylists", "ConversationId", "dbo.Conversations", "ConversationId", cascadeDelete: true);
        }
    }
}
