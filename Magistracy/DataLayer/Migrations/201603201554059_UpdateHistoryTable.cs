namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateHistoryTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NodeHistories", "SuggestionId", "dbo.Suggestions");
            DropIndex("dbo.NodeHistories", new[] { "SuggestionId" });
            AlterColumn("dbo.NodeHistories", "SuggestionId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NodeHistories", "SuggestionId", c => c.Int(nullable: false));
            CreateIndex("dbo.NodeHistories", "SuggestionId");
            AddForeignKey("dbo.NodeHistories", "SuggestionId", "dbo.Suggestions", "Id", cascadeDelete: true);
        }
    }
}
