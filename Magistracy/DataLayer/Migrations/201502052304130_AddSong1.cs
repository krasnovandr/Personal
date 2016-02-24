namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSong1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Songs", "AddDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Songs", "AddDate", c => c.DateTime(nullable: false));
        }
    }
}
