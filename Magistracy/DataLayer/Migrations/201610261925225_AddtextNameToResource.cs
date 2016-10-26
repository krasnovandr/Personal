namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtextNameToResource : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NodeResources", "TextName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NodeResources", "TextName");
        }
    }
}
