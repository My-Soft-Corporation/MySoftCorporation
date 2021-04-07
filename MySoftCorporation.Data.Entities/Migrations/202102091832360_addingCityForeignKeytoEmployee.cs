namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingCityForeignKeytoEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "CityID", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "CityID");
            AddForeignKey("dbo.Employees", "CityID", "dbo.Cities", "ID", cascadeDelete: true);
            DropColumn("dbo.Employees", "City");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "City", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.Employees", "CityID", "dbo.Cities");
            DropIndex("dbo.Employees", new[] { "CityID" });
            DropColumn("dbo.Employees", "CityID");
        }
    }
}
