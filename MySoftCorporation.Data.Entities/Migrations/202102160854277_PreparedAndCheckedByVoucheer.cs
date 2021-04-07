namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreparedAndCheckedByVoucheer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        VoucherID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        DateTime = c.DateTime(nullable: false),
                        Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ModifiedOn = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 150),
                        CheckedByVoucher_Id = c.String(maxLength: 128),
                        PreparedByVoucher_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VoucherID)
                .ForeignKey("dbo.AspNetUsers", t => t.CheckedByVoucher_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PreparedByVoucher_Id)
                .Index(t => t.CheckedByVoucher_Id)
                .Index(t => t.PreparedByVoucher_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vouchers", "PreparedByVoucher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vouchers", "CheckedByVoucher_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Vouchers", new[] { "PreparedByVoucher_Id" });
            DropIndex("dbo.Vouchers", new[] { "CheckedByVoucher_Id" });
            DropTable("dbo.Vouchers");
        }
    }
}
