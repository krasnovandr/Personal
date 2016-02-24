namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTableNewField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SongAtThisMoment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SongAtThisMoment");
        }
    }
}
