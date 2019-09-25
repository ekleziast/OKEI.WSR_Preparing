namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isDeletedDeal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deal", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deal", "isDeleted");
        }
    }
}
