namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewSongFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "BitRate", c => c.Int(nullable: false));
            AddColumn("dbo.Songs", "Duration", c => c.Time(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "Duration");
            DropColumn("dbo.Songs", "BitRate");
        }
    }
}
