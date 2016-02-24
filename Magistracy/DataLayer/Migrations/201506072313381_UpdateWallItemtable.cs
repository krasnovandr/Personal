namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWallItemtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WallItems", "Header", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WallItems", "Header");
        }
    }
}
