namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSongField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "AlbumAndTrackInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "AlbumAndTrackInfo");
        }
    }
}
