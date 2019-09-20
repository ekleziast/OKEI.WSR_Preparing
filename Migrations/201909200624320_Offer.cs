namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Offer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Offer", "ClientID", "dbo.Person");
            AddForeignKey("dbo.Offer", "ClientID", "dbo.Client", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offer", "ClientID", "dbo.Client");
            AddForeignKey("dbo.Offer", "ClientID", "dbo.Person", "ID", cascadeDelete: true);
        }
    }
}
