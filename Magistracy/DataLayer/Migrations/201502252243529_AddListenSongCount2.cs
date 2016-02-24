namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListenSongCount2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListenedSongs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        SongId = c.String(),
                        ListenDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ListenedSongs");
        }
    }
}
