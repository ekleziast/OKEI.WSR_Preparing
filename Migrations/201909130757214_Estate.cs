namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Estate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estate", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Estate", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Estate", "Number", c => c.String());
            AlterColumn("dbo.Estate", "Apartment", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Estate", "Apartment", c => c.Int());
            AlterColumn("dbo.Estate", "Number", c => c.Int(nullable: false));
            DropColumn("dbo.Estate", "Longitude");
            DropColumn("dbo.Estate", "Latitude");
        }
    }
}
