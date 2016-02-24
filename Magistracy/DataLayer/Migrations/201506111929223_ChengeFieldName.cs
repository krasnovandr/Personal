namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChengeFieldName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.WallItemLikeDislikes", name: "User_Id", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.WallItemLikeDislikes", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.WallItemLikeDislikes", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.WallItemLikeDislikes", name: "ApplicationUser_Id", newName: "User_Id");
        }
    }
}
