namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessageFields2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "FromAvatarPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "FromAvatarPath");
        }
    }
}
