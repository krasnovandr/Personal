namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewSongFields1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "Title");
        }
    }
}
