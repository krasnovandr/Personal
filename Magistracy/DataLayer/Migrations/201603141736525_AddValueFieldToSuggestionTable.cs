namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValueFieldToSuggestionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Suggestions", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suggestions", "Value");
        }
    }
}
