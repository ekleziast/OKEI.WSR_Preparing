namespace esoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLoginAndPassword : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Person", "Login");
            DropColumn("dbo.Person", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "Password", c => c.String());
            AddColumn("dbo.Person", "Login", c => c.String());
        }
    }
}
