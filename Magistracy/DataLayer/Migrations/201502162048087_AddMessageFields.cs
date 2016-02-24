namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessageFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "FromName", c => c.String());
            AddColumn("dbo.Messages", "FromId", c => c.String());
            DropColumn("dbo.Messages", "From");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "From", c => c.String());
            DropColumn("dbo.Messages", "FromId");
            DropColumn("dbo.Messages", "FromName");
        }
    }
}
