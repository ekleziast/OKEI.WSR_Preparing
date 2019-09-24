namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DemandFilterPriceAndArea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DemandFilter", "MinPrice", c => c.Int());
            AddColumn("dbo.DemandFilter", "MaxPrice", c => c.Int());
            AddColumn("dbo.DemandFilter", "MinArea", c => c.Int());
            AddColumn("dbo.DemandFilter", "MaxArea", c => c.Int());
            DropColumn("dbo.Demand", "MinPrice");
            DropColumn("dbo.Demand", "MaxPrice");
            DropColumn("dbo.Demand", "MinArea");
            DropColumn("dbo.Demand", "MaxArea");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Demand", "MaxArea", c => c.Int());
            AddColumn("dbo.Demand", "MinArea", c => c.Int());
            AddColumn("dbo.Demand", "MaxPrice", c => c.Int());
            AddColumn("dbo.Demand", "MinPrice", c => c.Int());
            DropColumn("dbo.DemandFilter", "MaxArea");
            DropColumn("dbo.DemandFilter", "MinArea");
            DropColumn("dbo.DemandFilter", "MaxPrice");
            DropColumn("dbo.DemandFilter", "MinPrice");
        }
    }
}
