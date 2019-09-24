namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DemandFilter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DemandFilter", "MinFloor", c => c.Int());
            AddColumn("dbo.DemandFilter", "MaxFloor", c => c.Int());
            DropColumn("dbo.DemandFilter", "MinArea");
            DropColumn("dbo.DemandFilter", "MaxArea");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DemandFilter", "MaxArea", c => c.Int());
            AddColumn("dbo.DemandFilter", "MinArea", c => c.Int());
            DropColumn("dbo.DemandFilter", "MaxFloor");
            DropColumn("dbo.DemandFilter", "MinFloor");
        }
    }
}
