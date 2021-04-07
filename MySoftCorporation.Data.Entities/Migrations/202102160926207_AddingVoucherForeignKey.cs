namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingVoucherForeignKey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VoucherTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserID = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            AddColumn("dbo.Vouchers", "VoucherTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Vouchers", "VoucherTypeID");
            AddForeignKey("dbo.Vouchers", "VoucherTypeID", "dbo.VoucherTypes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vouchers", "VoucherTypeID", "dbo.VoucherTypes");
            DropForeignKey("dbo.VoucherTypes", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.VoucherTypes", new[] { "UserID" });
            DropIndex("dbo.Vouchers", new[] { "VoucherTypeID" });
            DropColumn("dbo.Vouchers", "VoucherTypeID");
            DropTable("dbo.VoucherTypes");
        }
    }
}
