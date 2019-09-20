namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agent",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        DealShare = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Apartment",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Floor = c.Int(nullable: false),
                        Rooms = c.Int(nullable: false),
                        Area = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Estate", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Estate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Street = c.String(),
                        Number = c.Int(nullable: false),
                        Apartment = c.Int(),
                        EstateTypeID = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EstateType", t => t.EstateTypeID, cascadeDelete: true)
                .Index(t => t.EstateTypeID);
            
            CreateTable(
                "dbo.EstateType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Deal",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OfferID = c.Int(),
                        DemandID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Demand", t => t.DemandID)
                .ForeignKey("dbo.Offer", t => t.OfferID)
                .Index(t => t.OfferID)
                .Index(t => t.DemandID);
            
            CreateTable(
                "dbo.Demand",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MinPrice = c.Int(nullable: false),
                        MaxPrice = c.Int(nullable: false),
                        MinArea = c.Int(nullable: false),
                        MaxArea = c.Int(nullable: false),
                        EstateID = c.Int(nullable: false),
                        DemandFilterID = c.Int(),
                        ClientID = c.Int(nullable: false),
                        AgentID = c.Int(nullable: false),
                        isCompleted = c.Boolean(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agent", t => t.AgentID, cascadeDelete: true)
                .ForeignKey("dbo.Client", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Estate", t => t.EstateID, cascadeDelete: true)
                .Index(t => t.EstateID)
                .Index(t => t.ClientID)
                .Index(t => t.AgentID);
            
            CreateTable(
                "dbo.DemandFilter",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        MinFloors = c.Int(nullable: false),
                        MaxFloors = c.Int(nullable: false),
                        MinArea = c.Int(nullable: false),
                        MaxArea = c.Int(nullable: false),
                        MinRooms = c.Int(nullable: false),
                        MaxRooms = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Demand", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Offer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstateID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        AgentID = c.Int(nullable: false),
                        isCompleted = c.Boolean(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Agent", t => t.AgentID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Estate", t => t.EstateID, cascadeDelete: true)
                .Index(t => t.EstateID)
                .Index(t => t.ClientID)
                .Index(t => t.AgentID);
            
            CreateTable(
                "dbo.House",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Floors = c.Int(nullable: false),
                        Rooms = c.Int(nullable: false),
                        Area = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Estate", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Land",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Area = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Estate", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Land", "ID", "dbo.Estate");
            DropForeignKey("dbo.House", "ID", "dbo.Estate");
            DropForeignKey("dbo.Deal", "OfferID", "dbo.Offer");
            DropForeignKey("dbo.Offer", "EstateID", "dbo.Estate");
            DropForeignKey("dbo.Offer", "ClientID", "dbo.Person");
            DropForeignKey("dbo.Offer", "AgentID", "dbo.Agent");
            DropForeignKey("dbo.Deal", "DemandID", "dbo.Demand");
            DropForeignKey("dbo.Demand", "EstateID", "dbo.Estate");
            DropForeignKey("dbo.DemandFilter", "ID", "dbo.Demand");
            DropForeignKey("dbo.Demand", "ClientID", "dbo.Client");
            DropForeignKey("dbo.Demand", "AgentID", "dbo.Agent");
            DropForeignKey("dbo.Client", "ID", "dbo.Person");
            DropForeignKey("dbo.Apartment", "ID", "dbo.Estate");
            DropForeignKey("dbo.Estate", "EstateTypeID", "dbo.EstateType");
            DropForeignKey("dbo.Agent", "ID", "dbo.Person");
            DropIndex("dbo.Land", new[] { "ID" });
            DropIndex("dbo.House", new[] { "ID" });
            DropIndex("dbo.Offer", new[] { "AgentID" });
            DropIndex("dbo.Offer", new[] { "ClientID" });
            DropIndex("dbo.Offer", new[] { "EstateID" });
            DropIndex("dbo.DemandFilter", new[] { "ID" });
            DropIndex("dbo.Demand", new[] { "AgentID" });
            DropIndex("dbo.Demand", new[] { "ClientID" });
            DropIndex("dbo.Demand", new[] { "EstateID" });
            DropIndex("dbo.Deal", new[] { "DemandID" });
            DropIndex("dbo.Deal", new[] { "OfferID" });
            DropIndex("dbo.Client", new[] { "ID" });
            DropIndex("dbo.Estate", new[] { "EstateTypeID" });
            DropIndex("dbo.Apartment", new[] { "ID" });
            DropIndex("dbo.Agent", new[] { "ID" });
            DropTable("dbo.Land");
            DropTable("dbo.House");
            DropTable("dbo.Offer");
            DropTable("dbo.DemandFilter");
            DropTable("dbo.Demand");
            DropTable("dbo.Deal");
            DropTable("dbo.Client");
            DropTable("dbo.EstateType");
            DropTable("dbo.Estate");
            DropTable("dbo.Apartment");
            DropTable("dbo.Person");
            DropTable("dbo.Agent");
        }
    }
}
