namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AvatarFilePath", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestGenres", c => c.String());
            AddColumn("dbo.AspNetUsers", "BestAtrists", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BestAtrists");
            DropColumn("dbo.AspNetUsers", "BestGenres");
            DropColumn("dbo.AspNetUsers", "AvatarFilePath");
        }
    }
}
