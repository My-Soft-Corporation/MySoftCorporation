namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmailToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Email", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Email");
        }
    }
}
