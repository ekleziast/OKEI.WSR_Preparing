namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HouseApartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartment", "RoomsApartment", c => c.String());
            AddColumn("dbo.House", "RoomsHouse", c => c.String());
            DropColumn("dbo.Apartment", "Rooms");
            DropColumn("dbo.House", "Rooms");
        }
        
        public override void Down()
        {
            AddColumn("dbo.House", "Rooms", c => c.String());
            AddColumn("dbo.Apartment", "Rooms", c => c.String());
            DropColumn("dbo.House", "RoomsHouse");
            DropColumn("dbo.Apartment", "RoomsApartment");
        }
    }
}
