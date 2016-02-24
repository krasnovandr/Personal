namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSongFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "FileName");
        }
    }
}
