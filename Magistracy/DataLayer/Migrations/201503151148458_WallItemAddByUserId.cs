namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WallItemAddByUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WallItems", "AddByUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WallItems", "AddByUserId");
        }
    }
}
