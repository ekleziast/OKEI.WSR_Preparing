namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DealCommend : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Demand", "AgentID", "dbo.Agent");
            DropForeignKey("dbo.Demand", "ClientID", "dbo.Client");
            DropForeignKey("dbo.Offer", "AgentID", "dbo.Agent");
            AddColumn("dbo.Estate", "HouseNumber", c => c.String());
            AddColumn("dbo.Estate", "ApartmentNumber", c => c.String());
            AlterColumn("dbo.Agent", "DealShare", c => c.Int(nullable: false));
            AddForeignKey("dbo.Demand", "AgentID", "dbo.Agent", "ID");
            AddForeignKey("dbo.Demand", "ClientID", "dbo.Client", "ID");
            AddForeignKey("dbo.Offer", "AgentID", "dbo.Agent", "ID");
            DropColumn("dbo.Estate", "Number");
            DropColumn("dbo.Estate", "Apartment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Estate", "Apartment", c => c.String());
            AddColumn("dbo.Estate", "Number", c => c.String());
            DropForeignKey("dbo.Offer", "AgentID", "dbo.Agent");
            DropForeignKey("dbo.Demand", "ClientID", "dbo.Client");
            DropForeignKey("dbo.Demand", "AgentID", "dbo.Agent");
            AlterColumn("dbo.Agent", "DealShare", c => c.Int());
            DropColumn("dbo.Estate", "ApartmentNumber");
            DropColumn("dbo.Estate", "HouseNumber");
            AddForeignKey("dbo.Offer", "AgentID", "dbo.Agent", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Demand", "ClientID", "dbo.Client", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Demand", "AgentID", "dbo.Agent", "ID", cascadeDelete: true);
        }
    }
}
