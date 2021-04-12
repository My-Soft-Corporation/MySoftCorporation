namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableFeePaymentsAndPaymentsGateway : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeePayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdmissionId = c.Int(nullable: false),
                        PaymentGatewayId = c.Int(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Latitude = c.String(nullable: false, maxLength: 150),
                        Longitude = c.String(nullable: false, maxLength: 150),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admissions", t => t.AdmissionId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentGateways", t => t.PaymentGatewayId, cascadeDelete: true)
                .Index(t => t.AdmissionId)
                .Index(t => t.PaymentGatewayId);
            
            CreateTable(
                "dbo.PaymentGateways",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Bank = c.String(nullable: false, maxLength: 50),
                        Branch = c.String(maxLength: 50),
                        AccountTitle = c.String(nullable: false, maxLength: 50),
                        AccountNumber = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                        UserID = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.Bank, unique: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeePayments", "PaymentGatewayId", "dbo.PaymentGateways");
            DropForeignKey("dbo.PaymentGateways", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FeePayments", "AdmissionId", "dbo.Admissions");
            DropIndex("dbo.PaymentGateways", new[] { "UserID" });
            DropIndex("dbo.PaymentGateways", new[] { "Bank" });
            DropIndex("dbo.FeePayments", new[] { "PaymentGatewayId" });
            DropIndex("dbo.FeePayments", new[] { "AdmissionId" });
            DropTable("dbo.PaymentGateways");
            DropTable("dbo.FeePayments");
        }
    }
}
