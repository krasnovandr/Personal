namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVkFieldsToUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "VkLogin", c => c.String());
            AddColumn("dbo.AspNetUsers", "VkPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "VkPassword");
            DropColumn("dbo.AspNetUsers", "VkLogin");
        }
    }
}
