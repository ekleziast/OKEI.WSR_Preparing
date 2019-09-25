namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notNullEstateType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Estate", "EstateTypeID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Estate", "EstateTypeID", c => c.Int());
        }
    }
}
