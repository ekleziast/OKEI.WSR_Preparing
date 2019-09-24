namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntToDoubleInDemandFilter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DemandFilter", "MinArea", c => c.Double());
            AlterColumn("dbo.DemandFilter", "MaxArea", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DemandFilter", "MaxArea", c => c.Int());
            AlterColumn("dbo.DemandFilter", "MinArea", c => c.Int());
        }
    }
}
