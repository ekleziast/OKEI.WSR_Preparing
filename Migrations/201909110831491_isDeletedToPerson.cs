namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isDeletedToPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "isDeleted");
        }
    }
}
