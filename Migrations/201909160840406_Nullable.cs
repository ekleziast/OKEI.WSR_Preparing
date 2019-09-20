namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Apartment", "Floor", c => c.Int());
            AlterColumn("dbo.Apartment", "Rooms", c => c.Int());
            AlterColumn("dbo.Estate", "Area", c => c.Double());
            AlterColumn("dbo.Estate", "Latitude", c => c.Double());
            AlterColumn("dbo.Estate", "Longitude", c => c.Double());
            AlterColumn("dbo.House", "Floors", c => c.Int());
            AlterColumn("dbo.House", "Rooms", c => c.Int());
            AlterColumn("dbo.Demand", "MinPrice", c => c.Int());
            AlterColumn("dbo.Demand", "MaxPrice", c => c.Int());
            AlterColumn("dbo.Demand", "MinArea", c => c.Int());
            AlterColumn("dbo.Demand", "MaxArea", c => c.Int());
            AlterColumn("dbo.DemandFilter", "MinFloors", c => c.Int());
            AlterColumn("dbo.DemandFilter", "MaxFloors", c => c.Int());
            AlterColumn("dbo.DemandFilter", "MinArea", c => c.Int());
            AlterColumn("dbo.DemandFilter", "MaxArea", c => c.Int());
            AlterColumn("dbo.DemandFilter", "MinRooms", c => c.Int());
            AlterColumn("dbo.DemandFilter", "MaxRooms", c => c.Int());
            AlterColumn("dbo.Offer", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Offer", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DemandFilter", "MaxRooms", c => c.Int(nullable: false));
            AlterColumn("dbo.DemandFilter", "MinRooms", c => c.Int(nullable: false));
            AlterColumn("dbo.DemandFilter", "MaxArea", c => c.Int(nullable: false));
            AlterColumn("dbo.DemandFilter", "MinArea", c => c.Int(nullable: false));
            AlterColumn("dbo.DemandFilter", "MaxFloors", c => c.Int(nullable: false));
            AlterColumn("dbo.DemandFilter", "MinFloors", c => c.Int(nullable: false));
            AlterColumn("dbo.Demand", "MaxArea", c => c.Int(nullable: false));
            AlterColumn("dbo.Demand", "MinArea", c => c.Int(nullable: false));
            AlterColumn("dbo.Demand", "MaxPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Demand", "MinPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.House", "Rooms", c => c.Int(nullable: false));
            AlterColumn("dbo.House", "Floors", c => c.Int(nullable: false));
            AlterColumn("dbo.Estate", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Estate", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Estate", "Area", c => c.String());
            AlterColumn("dbo.Apartment", "Rooms", c => c.Int(nullable: false));
            AlterColumn("dbo.Apartment", "Floor", c => c.Int(nullable: false));
        }
    }
}
