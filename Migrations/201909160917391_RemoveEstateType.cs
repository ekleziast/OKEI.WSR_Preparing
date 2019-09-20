namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEstateType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Estate", "EstateTypeID", "dbo.EstateType");
            DropIndex("dbo.Estate", new[] { "EstateTypeID" });
            DropColumn("dbo.Estate", "EstateTypeID");
            DropTable("dbo.EstateType");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EstateType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Estate", "EstateTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Estate", "EstateTypeID");
            AddForeignKey("dbo.Estate", "EstateTypeID", "dbo.EstateType", "ID", cascadeDelete: true);
        }
    }
}
