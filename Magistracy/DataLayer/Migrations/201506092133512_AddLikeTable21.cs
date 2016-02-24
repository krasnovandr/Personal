namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLikeTable21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WallItemLikeDislikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Like = c.Boolean(nullable: false),
                        DisLike = c.Boolean(nullable: false),
                        Date = c.DateTime(),
                        User_Id = c.String(maxLength: 128),
                        WallItem_WallItemId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.WallItems", t => t.WallItem_WallItemId)
                .Index(t => t.User_Id)
                .Index(t => t.WallItem_WallItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WallItemLikeDislikes", "WallItem_WallItemId", "dbo.WallItems");
            DropForeignKey("dbo.WallItemLikeDislikes", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.WallItemLikeDislikes", new[] { "WallItem_WallItemId" });
            DropIndex("dbo.WallItemLikeDislikes", new[] { "User_Id" });
            DropTable("dbo.WallItemLikeDislikes");
        }
    }
}
