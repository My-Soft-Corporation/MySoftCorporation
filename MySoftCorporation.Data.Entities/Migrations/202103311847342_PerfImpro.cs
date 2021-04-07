namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PerfImpro : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vouchers", "VoucherTypeID", "dbo.VoucherTypes");
            DropForeignKey("dbo.Vouchers", "CheckedByVoucher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vouchers", "PreparedByVoucher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EAttendances", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.VoucherTypes", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Vouchers", new[] { "VoucherTypeID" });
            DropIndex("dbo.Vouchers", new[] { "CheckedByVoucher_Id" });
            DropIndex("dbo.Vouchers", new[] { "PreparedByVoucher_Id" });
            DropIndex("dbo.VoucherTypes", new[] { "UserID" });
            DropIndex("dbo.EAttendances", new[] { "UserID" });
            RenameColumn(table: "dbo.VoucherTypes", name: "UserID", newName: "AddedById");
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "DateOfJoined", c => c.DateTime());
            AddColumn("dbo.VoucherTypes", "DateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.VoucherTypes", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.VoucherTypes", "AddedById", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.VoucherTypes", "AddedById");
            AddForeignKey("dbo.VoucherTypes", "AddedById", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.VoucherTypes", "ModifiedOn");
            DropTable("dbo.Vouchers");
            DropTable("dbo.EAttendances");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EAttendances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClockIn = c.DateTime(nullable: false),
                        CheckOut = c.DateTime(),
                        CheckIn = c.DateTime(),
                        ClockOut = c.DateTime(),
                        Date = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 4),
                        UserID = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        VoucherTypeID = c.Int(nullable: false),
                        CheckedByVoucher_Id = c.String(maxLength: 128),
                        PreparedByVoucher_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VoucherID);
            
            AddColumn("dbo.VoucherTypes", "ModifiedOn", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.VoucherTypes", "AddedById", "dbo.AspNetUsers");
            DropIndex("dbo.VoucherTypes", new[] { "AddedById" });
            AlterColumn("dbo.VoucherTypes", "AddedById", c => c.String(maxLength: 128));
            AlterColumn("dbo.VoucherTypes", "Name", c => c.String());
            DropColumn("dbo.VoucherTypes", "DateTime");
            DropColumn("dbo.AspNetUsers", "DateOfJoined");
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            RenameColumn(table: "dbo.VoucherTypes", name: "AddedById", newName: "UserID");
            CreateIndex("dbo.EAttendances", "UserID");
            CreateIndex("dbo.VoucherTypes", "UserID");
            CreateIndex("dbo.Vouchers", "PreparedByVoucher_Id");
            CreateIndex("dbo.Vouchers", "CheckedByVoucher_Id");
            CreateIndex("dbo.Vouchers", "VoucherTypeID");
            AddForeignKey("dbo.VoucherTypes", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.EAttendances", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Vouchers", "PreparedByVoucher_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Vouchers", "CheckedByVoucher_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Vouchers", "VoucherTypeID", "dbo.VoucherTypes", "ID", cascadeDelete: true);
        }
    }
}
