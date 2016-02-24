namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateListenedSongFields2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ListenedSongs", "ListenCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ListenedSongs", "ListenCount", c => c.Int(nullable: false));
        }
    }
}
