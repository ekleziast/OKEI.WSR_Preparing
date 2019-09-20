namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstateArea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estate", "Area", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Estate", "Area");
        }
    }
}
