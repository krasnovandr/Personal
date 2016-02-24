namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTagtable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Songs", "Tag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "Tag", c => c.String());
        }
    }
}
