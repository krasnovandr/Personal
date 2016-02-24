namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChengeFieldName2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WallItemLikeDislikes", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WallItemLikeDislikes", "UserId");
        }
    }
}
