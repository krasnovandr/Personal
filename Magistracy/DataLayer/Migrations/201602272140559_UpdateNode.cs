namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nodes", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Nodes", "CreatedBy");
        }
    }
}
