namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_NodeResourceTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NodeResources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ResourceRaw = c.String(),
                        Resource = c.String(),
                        AddBy_Id = c.String(maxLength: 128),
                        Node_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AddBy_Id)
                .ForeignKey("dbo.SessionNodes", t => t.Node_Id)
                .Index(t => t.AddBy_Id)
                .Index(t => t.Node_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NodeResources", "Node_Id", "dbo.SessionNodes");
            DropForeignKey("dbo.NodeResources", "AddBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.NodeResources", new[] { "Node_Id" });
            DropIndex("dbo.NodeResources", new[] { "AddBy_Id" });
            DropTable("dbo.NodeResources");
        }
    }
}
