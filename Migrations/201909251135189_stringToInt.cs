namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Apartment", "Floor", c => c.Int());
            AlterColumn("dbo.Apartment", "RoomsApartment", c => c.Int());
            AlterColumn("dbo.House", "Floors", c => c.Int());
            AlterColumn("dbo.House", "RoomsHouse", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.House", "RoomsHouse", c => c.String());
            AlterColumn("dbo.House", "Floors", c => c.String());
            AlterColumn("dbo.Apartment", "RoomsApartment", c => c.String());
            AlterColumn("dbo.Apartment", "Floor", c => c.String());
        }
    }
}
