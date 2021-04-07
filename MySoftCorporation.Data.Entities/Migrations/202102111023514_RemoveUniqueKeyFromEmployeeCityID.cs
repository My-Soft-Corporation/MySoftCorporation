namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUniqueKeyFromEmployeeCityID : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Employees", new[] { "CityID" });
            CreateIndex("dbo.Employees", "CityID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", new[] { "CityID" });
            CreateIndex("dbo.Employees", "CityID", unique: true);
        }
    }
}
