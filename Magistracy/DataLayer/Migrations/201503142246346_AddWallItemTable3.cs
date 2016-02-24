namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWallItemTable3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.WallItemsSongs", newName: "SongWallItems");
            RenameColumn(table: "dbo.SongWallItems", name: "SongId", newName: "WallItem_WallItemId");
            RenameColumn(table: "dbo.SongWallItems", name: "WallItemId", newName: "Song_SongId");
            RenameIndex(table: "dbo.SongWallItems", name: "IX_WallItemId", newName: "IX_Song_SongId");
            RenameIndex(table: "dbo.SongWallItems", name: "IX_SongId", newName: "IX_WallItem_WallItemId");
            DropPrimaryKey("dbo.SongWallItems");
            AddPrimaryKey("dbo.SongWallItems", new[] { "Song_SongId", "WallItem_WallItemId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SongWallItems");
            AddPrimaryKey("dbo.SongWallItems", new[] { "SongId", "WallItemId" });
            RenameIndex(table: "dbo.SongWallItems", name: "IX_WallItem_WallItemId", newName: "IX_SongId");
            RenameIndex(table: "dbo.SongWallItems", name: "IX_Song_SongId", newName: "IX_WallItemId");
            RenameColumn(table: "dbo.SongWallItems", name: "Song_SongId", newName: "WallItemId");
            RenameColumn(table: "dbo.SongWallItems", name: "WallItem_WallItemId", newName: "SongId");
            RenameTable(name: "dbo.SongWallItems", newName: "WallItemsSongs");
        }
    }
}
