namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedIsCompletedInOfferAndDemand : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Demand", "isCompleted");
            DropColumn("dbo.Offer", "isCompleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Offer", "isCompleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Demand", "isCompleted", c => c.Boolean(nullable: false));
        }
    }
}
