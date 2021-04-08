 namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LedgerImprovmenet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ledgers", "Latitude", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Ledgers", "Longitude", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Ledgers", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ledgers", "Location", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Ledgers", "Longitude");
            DropColumn("dbo.Ledgers", "Latitude");
        }
    }
}
