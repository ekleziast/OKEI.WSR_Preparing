namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Estates : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Apartment", "Area");
            DropColumn("dbo.House", "Area");
            DropColumn("dbo.Land", "Area");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Land", "Area", c => c.Int(nullable: false));
            AddColumn("dbo.House", "Area", c => c.Int(nullable: false));
            AddColumn("dbo.Apartment", "Area", c => c.Int(nullable: false));
        }
    }
}
