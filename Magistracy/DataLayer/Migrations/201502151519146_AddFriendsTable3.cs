namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFriendsTable3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Friends");
            AlterColumn("dbo.Friends", "Id", c => c.String());
            AlterColumn("dbo.Friends", "RecordId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Friends", "RecordId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Friends");
            AlterColumn("dbo.Friends", "RecordId", c => c.String());
            AlterColumn("dbo.Friends", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Friends", "Id");
        }
    }
}
