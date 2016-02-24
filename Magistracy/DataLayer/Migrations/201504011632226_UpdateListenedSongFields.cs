namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateListenedSongFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ListenedSongs", "ListenCount", c => c.Int(nullable: false));
            AddColumn("dbo.Songs", "AddBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "AddBy");
            DropColumn("dbo.ListenedSongs", "ListenCount");
        }
    }
}
