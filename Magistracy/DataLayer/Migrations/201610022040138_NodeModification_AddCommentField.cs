namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NodeModification_AddCommentField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NodeModifications", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NodeModifications", "Comment");
        }
    }
}
