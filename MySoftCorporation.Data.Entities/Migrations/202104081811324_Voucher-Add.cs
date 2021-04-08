namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VoucherAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        DateTime = c.DateTime(nullable: false),
                        Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VoucherTypeId = c.Int(nullable: false),
                        PreparedById = c.String(nullable: false, maxLength: 128),
                        CheckedById = c.String(nullable: false, maxLength: 128),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Latitude = c.String(nullable: false, maxLength: 150),
                        Longitude = c.String(nullable: false, maxLength: 150),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CheckedById, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.PreparedById, cascadeDelete: false)
                .ForeignKey("dbo.VoucherTypes", t => t.VoucherTypeId, cascadeDelete:false )
                .Index(t => t.VoucherTypeId)
                .Index(t => t.PreparedById)
                .Index(t => t.CheckedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vouchers", "VoucherTypeId", "dbo.VoucherTypes");
            DropForeignKey("dbo.Vouchers", "PreparedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vouchers", "CheckedById", "dbo.AspNetUsers");
            DropIndex("dbo.Vouchers", new[] { "CheckedById" });
            DropIndex("dbo.Vouchers", new[] { "PreparedById" });
            DropIndex("dbo.Vouchers", new[] { "VoucherTypeId" });
            DropTable("dbo.Vouchers");
        }
    }
}
