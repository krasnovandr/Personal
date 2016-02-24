namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeUserFields3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "YourTop10Tracks", c => c.String());
            AddColumn("dbo.AspNetUsers", "MusicWhenBoring", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestCinemaSoundtrack", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestGameSoundtrack", c => c.String());
            AddColumn("dbo.AspNetUsers", "YourSongsOrGroup", c => c.String());
            AddColumn("dbo.AspNetUsers", "YourBestMusicInstrument", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestMusicWhenHappy", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestMusicWhenBorring", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BestMusicWhenBorring");
            DropColumn("dbo.AspNetUsers", "BestMusicWhenHappy");
            DropColumn("dbo.AspNetUsers", "YourBestMusicInstrument");
            DropColumn("dbo.AspNetUsers", "YourSongsOrGroup");
            DropColumn("dbo.AspNetUsers", "BestGameSoundtrack");
            DropColumn("dbo.AspNetUsers", "BestCinemaSoundtrack");
            DropColumn("dbo.AspNetUsers", "MusicWhenBoring");
            DropColumn("dbo.AspNetUsers", "YourTop10Tracks");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "BirthDate");
        }
    }
}
