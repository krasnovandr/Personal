namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPlaylistsToConversation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConversationPlaylists",
                c => new
                    {
                        ConversationId = c.String(nullable: false, maxLength: 128),
                        PlaylistId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ConversationId, t.PlaylistId })
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .ForeignKey("dbo.Playlists", t => t.PlaylistId, cascadeDelete: true)
                .Index(t => t.ConversationId)
                .Index(t => t.PlaylistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConversationPlaylists", "PlaylistId", "dbo.Playlists");
            DropForeignKey("dbo.ConversationPlaylists", "ConversationId", "dbo.Conversations");
            DropIndex("dbo.ConversationPlaylists", new[] { "PlaylistId" });
            DropIndex("dbo.ConversationPlaylists", new[] { "ConversationId" });
            DropTable("dbo.ConversationPlaylists");
        }
    }
}
