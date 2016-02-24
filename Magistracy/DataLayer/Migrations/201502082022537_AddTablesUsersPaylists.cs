namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTablesUsersPaylists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersPlaylists",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        PlaylistId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.PlaylistId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Playlists", t => t.PlaylistId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PlaylistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersPlaylists", "PlaylistId", "dbo.Playlists");
            DropForeignKey("dbo.UsersPlaylists", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UsersPlaylists", new[] { "PlaylistId" });
            DropIndex("dbo.UsersPlaylists", new[] { "UserId" });
            DropTable("dbo.UsersPlaylists");
        }
    }
}
