namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addConversationCurrenSong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "SongAtThisMoment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "SongAtThisMoment");
        }
    }
}
