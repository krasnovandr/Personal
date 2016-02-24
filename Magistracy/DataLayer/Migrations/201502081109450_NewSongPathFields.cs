namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewSongPathFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "SongPath", c => c.String());
            AddColumn("dbo.Songs", "SongAlbumCoverPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "SongAlbumCoverPath");
            DropColumn("dbo.Songs", "SongPath");
        }
    }
}
