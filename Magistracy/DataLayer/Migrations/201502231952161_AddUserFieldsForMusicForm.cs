namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserFieldsForMusicForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestGenre", c => c.String());
            AddColumn("dbo.AspNetUsers", "WorstGenre", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestForeignArtist", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestNativeArtist", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastEntrenchedSong", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestVocalist", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestAlarmClock", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestSleeping", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestRelaxSong", c => c.String());
            DropColumn("dbo.AspNetUsers", "BestGenres");
            DropColumn("dbo.AspNetUsers", "BestAtrists");
            DropColumn("dbo.AspNetUsers", "YourTop10Tracks");
            DropColumn("dbo.AspNetUsers", "MusicWhenBoring");
            DropColumn("dbo.AspNetUsers", "YourSongsOrGroup");
            DropColumn("dbo.AspNetUsers", "YourBestMusicInstrument");
            DropColumn("dbo.AspNetUsers", "BestMusicWhenHappy");
            DropColumn("dbo.AspNetUsers", "BestMusicWhenBorring");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "BestMusicWhenBorring", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestMusicWhenHappy", c => c.String());
            AddColumn("dbo.AspNetUsers", "YourBestMusicInstrument", c => c.String());
            AddColumn("dbo.AspNetUsers", "YourSongsOrGroup", c => c.String());
            AddColumn("dbo.AspNetUsers", "MusicWhenBoring", c => c.String());
            AddColumn("dbo.AspNetUsers", "YourTop10Tracks", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestAtrists", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestGenres", c => c.String());
            DropColumn("dbo.AspNetUsers", "BestRelaxSong");
            DropColumn("dbo.AspNetUsers", "BestSleeping");
            DropColumn("dbo.AspNetUsers", "BestAlarmClock");
            DropColumn("dbo.AspNetUsers", "BestVocalist");
            DropColumn("dbo.AspNetUsers", "LastEntrenchedSong");
            DropColumn("dbo.AspNetUsers", "BestNativeArtist");
            DropColumn("dbo.AspNetUsers", "BestForeignArtist");
            DropColumn("dbo.AspNetUsers", "WorstGenre");
            DropColumn("dbo.AspNetUsers", "BestGenre");
            DropColumn("dbo.AspNetUsers", "Country");
            DropColumn("dbo.AspNetUsers", "City");
        }
    }
}
