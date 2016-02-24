namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Playlists", "DefaultPlaylist2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Playlists", "DefaultPlaylist2", c => c.Boolean(nullable: false));
        }
    }
}
