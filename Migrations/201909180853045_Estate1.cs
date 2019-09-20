namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Estate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Estate", "EstateTypeID", "dbo.EstateType");
            DropIndex("dbo.Estate", new[] { "EstateTypeID" });
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
            
            CreateIndex("dbo.Estate", "EstateTypeID");
            AddForeignKey("dbo.Estate", "EstateTypeID", "dbo.EstateType", "ID");
        }
    }
}
