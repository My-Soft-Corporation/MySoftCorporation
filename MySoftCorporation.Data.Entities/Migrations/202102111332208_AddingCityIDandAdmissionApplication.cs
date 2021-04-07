namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCityIDandAdmissionApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "MaritalStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "CityID", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "CityID");
            AddForeignKey("dbo.Students", "CityID", "dbo.Cities", "ID", cascadeDelete: false);
            DropColumn("dbo.Students", "Country");
            DropColumn("dbo.Students", "State");
            DropColumn("dbo.Students", "City");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "City", c => c.String(nullable: false, maxLength: 80));
            AddColumn("dbo.Students", "State", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Students", "Country", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.Students", "CityID", "dbo.Cities");
            DropIndex("dbo.Students", new[] { "CityID" });
            DropColumn("dbo.Students", "CityID");
            DropColumn("dbo.Students", "MaritalStatus");
        }
    }
}
