namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessageSongsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageSongs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SongId = c.String(),
                        MessageId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MessageSongs");
        }
    }
}
