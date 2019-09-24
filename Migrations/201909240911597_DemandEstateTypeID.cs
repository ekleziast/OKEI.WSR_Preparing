namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DemandEstateTypeID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Demand", "EstateTypeID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Demand", "EstateTypeID");
        }
    }
}
