namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFieldPlaylist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlaylistItems", "TrackPos", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlaylistItems", "TrackPos");
        }
    }
}
