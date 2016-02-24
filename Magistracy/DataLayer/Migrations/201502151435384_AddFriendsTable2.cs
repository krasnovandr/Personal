namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFriendsTable2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "RecordId", c => c.String());
            DropColumn("dbo.Friends", "MyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "MyId", c => c.String());
            DropColumn("dbo.Friends", "RecordId");
        }
    }
}
