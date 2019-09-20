namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estateIntToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Apartment", "Floor", c => c.String());
            AlterColumn("dbo.Apartment", "Rooms", c => c.String());
            AlterColumn("dbo.House", "Floors", c => c.String());
            AlterColumn("dbo.House", "Rooms", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.House", "Rooms", c => c.Int());
            AlterColumn("dbo.House", "Floors", c => c.Int());
            AlterColumn("dbo.Apartment", "Rooms", c => c.Int());
            AlterColumn("dbo.Apartment", "Floor", c => c.Int());
        }
    }
}
