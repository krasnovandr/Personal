namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletePlaylistFromSong : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Playlists", "Song_SongId", "dbo.Songs");
            DropIndex("dbo.Playlists", new[] { "Song_SongId" });
            DropColumn("dbo.Playlists", "Song_SongId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Playlists", "Song_SongId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Playlists", "Song_SongId");
            AddForeignKey("dbo.Playlists", "Song_SongId", "dbo.Songs", "SongId");
        }
    }
}
