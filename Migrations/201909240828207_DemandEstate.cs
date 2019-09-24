namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DemandEstate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Demand", "EstateID", "dbo.Estate");
            DropIndex("dbo.Demand", new[] { "EstateID" });
            DropColumn("dbo.Demand", "EstateID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Demand", "EstateID", c => c.Int(nullable: false));
            CreateIndex("dbo.Demand", "EstateID");
            AddForeignKey("dbo.Demand", "EstateID", "dbo.Estate", "ID", cascadeDelete: true);
        }
    }
}
