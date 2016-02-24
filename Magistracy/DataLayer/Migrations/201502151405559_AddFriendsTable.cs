namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFriendsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlaylistItems", "PlaylistId", "dbo.Playlists");
            DropIndex("dbo.PlaylistItems", new[] { "PlaylistId" });
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MyId = c.String(),
                        FriendId = c.String(),
                        AddDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.PlaylistItems", "PlaylistId", c => c.String(maxLength: 128));
            CreateIndex("dbo.PlaylistItems", "PlaylistId");
            AddForeignKey("dbo.PlaylistItems", "PlaylistId", "dbo.Playlists", "PlaylistId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlaylistItems", "PlaylistId", "dbo.Playlists");
            DropIndex("dbo.PlaylistItems", new[] { "PlaylistId" });
            AlterColumn("dbo.PlaylistItems", "PlaylistId", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.Friends");
            CreateIndex("dbo.PlaylistItems", "PlaylistId");
            AddForeignKey("dbo.PlaylistItems", "PlaylistId", "dbo.Playlists", "PlaylistId", cascadeDelete: true);
        }
    }
}
