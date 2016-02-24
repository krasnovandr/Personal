namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTagtable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TagsLikes", name: "LikeBy_Id", newName: "User_Id");
            RenameIndex(table: "dbo.TagsLikes", name: "IX_LikeBy_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TagsLikes", name: "IX_User_Id", newName: "IX_LikeBy_Id");
            RenameColumn(table: "dbo.TagsLikes", name: "User_Id", newName: "LikeBy_Id");
        }
    }
}
