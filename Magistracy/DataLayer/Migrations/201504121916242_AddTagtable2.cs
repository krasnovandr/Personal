namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTagtable2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagsLikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LikeBy_Id = c.String(maxLength: 128),
                        Song_SongId = c.String(maxLength: 128),
                        Tag_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.LikeBy_Id)
                .ForeignKey("dbo.Songs", t => t.Song_SongId)
                .ForeignKey("dbo.Tags", t => t.Tag_Id)
                .Index(t => t.LikeBy_Id)
                .Index(t => t.Song_SongId)
                .Index(t => t.Tag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagsLikes", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TagsLikes", "Song_SongId", "dbo.Songs");
            DropForeignKey("dbo.TagsLikes", "LikeBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TagsLikes", new[] { "Tag_Id" });
            DropIndex("dbo.TagsLikes", new[] { "Song_SongId" });
            DropIndex("dbo.TagsLikes", new[] { "LikeBy_Id" });
            DropTable("dbo.TagsLikes");
            DropTable("dbo.Tags");
        }
    }
}
