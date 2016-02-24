namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SongNewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "Copyright", c => c.String());
            AddColumn("dbo.Songs", "DiscCount", c => c.Int(nullable: false));
            AddColumn("dbo.Songs", "Composers", c => c.String());
            AddColumn("dbo.Songs", "Lyrics", c => c.String());
            AddColumn("dbo.Songs", "Disc", c => c.Int(nullable: false));
            AddColumn("dbo.Songs", "Performers", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "Performers");
            DropColumn("dbo.Songs", "Disc");
            DropColumn("dbo.Songs", "Lyrics");
            DropColumn("dbo.Songs", "Composers");
            DropColumn("dbo.Songs", "DiscCount");
            DropColumn("dbo.Songs", "Copyright");
        }
    }
}
