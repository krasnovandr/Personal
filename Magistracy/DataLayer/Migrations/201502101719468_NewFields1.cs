namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFields1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Playlists", "DefaultPlaylist", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Playlists", "DefaultPlaylist");
        }
    }
}
