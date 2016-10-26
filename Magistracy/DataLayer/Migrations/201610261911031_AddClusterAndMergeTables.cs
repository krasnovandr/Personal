namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClusterAndMergeTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResourceClusters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ClusterNumber = c.Int(nullable: false),
                        HierarchicalClusteringPath = c.String(),
                        Node_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SessionNodes", t => t.Node_Id)
                .Index(t => t.Node_Id);
            
            CreateTable(
                "dbo.ClusterMergeResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstResourceId = c.Int(),
                        SecondResourceId = c.Int(),
                        Cluster_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResourceClusters", t => t.Cluster_Id)
                .Index(t => t.Cluster_Id);
            
            AddColumn("dbo.SessionNodes", "ClusterImagePath", c => c.String());
            AddColumn("dbo.SessionNodes", "WordCloudImagePath", c => c.String());
            AddColumn("dbo.NodeResources", "Cluster_Id", c => c.Int());
            CreateIndex("dbo.NodeResources", "Cluster_Id");
            AddForeignKey("dbo.NodeResources", "Cluster_Id", "dbo.ResourceClusters", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NodeResources", "Cluster_Id", "dbo.ResourceClusters");
            DropForeignKey("dbo.ResourceClusters", "Node_Id", "dbo.SessionNodes");
            DropForeignKey("dbo.ClusterMergeResults", "Cluster_Id", "dbo.ResourceClusters");
            DropIndex("dbo.NodeResources", new[] { "Cluster_Id" });
            DropIndex("dbo.ClusterMergeResults", new[] { "Cluster_Id" });
            DropIndex("dbo.ResourceClusters", new[] { "Node_Id" });
            DropColumn("dbo.NodeResources", "Cluster_Id");
            DropColumn("dbo.SessionNodes", "WordCloudImagePath");
            DropColumn("dbo.SessionNodes", "ClusterImagePath");
            DropTable("dbo.ClusterMergeResults");
            DropTable("dbo.ResourceClusters");
        }
    }
}
