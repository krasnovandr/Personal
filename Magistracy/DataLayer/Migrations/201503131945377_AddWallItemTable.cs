namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWallItemTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WallItems",
                c => new
                    {
                        WallItemId = c.Int(nullable: false, identity: true),
                        Note = c.String(),
                        AddDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.WallItemId);
            
            CreateTable(
                "dbo.WallItemsSongs",
                c => new
                    {
                        SongId = c.Int(nullable: false),
                        WallItemId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.SongId, t.WallItemId })
                .ForeignKey("dbo.WallItems", t => t.SongId, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.WallItemId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.WallItemId);
            
            CreateTable(
                "dbo.UsersWallItems",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        WallItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.WallItemId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.WallItems", t => t.WallItemId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.WallItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersWallItems", "WallItemId", "dbo.WallItems");
            DropForeignKey("dbo.UsersWallItems", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.WallItemsSongs", "WallItemId", "dbo.Songs");
            DropForeignKey("dbo.WallItemsSongs", "SongId", "dbo.WallItems");
            DropIndex("dbo.UsersWallItems", new[] { "WallItemId" });
            DropIndex("dbo.UsersWallItems", new[] { "UserId" });
            DropIndex("dbo.WallItemsSongs", new[] { "WallItemId" });
            DropIndex("dbo.WallItemsSongs", new[] { "SongId" });
            DropTable("dbo.UsersWallItems");
            DropTable("dbo.WallItemsSongs");
            DropTable("dbo.WallItems");
        }
    }
}
