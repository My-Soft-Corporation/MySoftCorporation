namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accountTypeAndChartOfAccounts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        AddedById = c.String(nullable: false, maxLength: 128),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Latitude = c.String(nullable: false, maxLength: 150),
                        Longitude = c.String(nullable: false, maxLength: 150),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedById, cascadeDelete: true)
                .Index(t => t.AddedById);
            
            CreateTable(
                "dbo.ChartOfAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 40),
                        Code = c.String(nullable: false, maxLength: 6),
                        Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserID = c.String(maxLength: 128),
                        AccountTypeId = c.Int(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Latitude = c.String(nullable: false, maxLength: 150),
                        Longitude = c.String(nullable: false, maxLength: 150),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountTypes", t => t.AccountTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.Title, unique: true)
                .Index(t => t.Code, unique: true)
                .Index(t => t.UserID)
                .Index(t => t.AccountTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChartOfAccounts", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChartOfAccounts", "AccountTypeId", "dbo.AccountTypes");
            DropForeignKey("dbo.AccountTypes", "AddedById", "dbo.AspNetUsers");
            DropIndex("dbo.ChartOfAccounts", new[] { "AccountTypeId" });
            DropIndex("dbo.ChartOfAccounts", new[] { "UserID" });
            DropIndex("dbo.ChartOfAccounts", new[] { "Code" });
            DropIndex("dbo.ChartOfAccounts", new[] { "Title" });
            DropIndex("dbo.AccountTypes", new[] { "AddedById" });
            DropTable("dbo.ChartOfAccounts");
            DropTable("dbo.AccountTypes");
        }
    }
}
