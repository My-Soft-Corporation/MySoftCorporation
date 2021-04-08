namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VoucherType_LocationParam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VoucherTypes", "Latitude", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.VoucherTypes", "Longitude", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.VoucherTypes", "ModifiedOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.VoucherTypes", "Location");
            DropColumn("dbo.VoucherTypes", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VoucherTypes", "DateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.VoucherTypes", "Location", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.VoucherTypes", "ModifiedOn");
            DropColumn("dbo.VoucherTypes", "Longitude");
            DropColumn("dbo.VoucherTypes", "Latitude");
        }
    }
}
