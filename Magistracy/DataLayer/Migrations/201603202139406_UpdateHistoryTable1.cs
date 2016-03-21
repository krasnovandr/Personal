namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateHistoryTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NodeHistories", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NodeHistories", "Value");
        }
    }
}
