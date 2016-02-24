namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFriendsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "UserId", c => c.String());
            AddColumn("dbo.Friends", "Confirmed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Friends", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "Id", c => c.String());
            DropColumn("dbo.Friends", "Confirmed");
            DropColumn("dbo.Friends", "UserId");
        }
    }
}
