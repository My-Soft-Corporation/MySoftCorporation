namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_StudentLedgerAndStudentLedgerTransactionAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentLedgers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 40),
                        Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StudentId = c.Int(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 80),
                        Latitude = c.String(nullable: false, maxLength: 50),
                        Longitude = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId, unique: true);
            
            CreateTable(
                "dbo.StudentLedgerTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 40),
                        Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StudentLedgerId = c.Int(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 80),
                        Latitude = c.String(nullable: false, maxLength: 50),
                        Longitude = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentLedgers", t => t.StudentLedgerId, cascadeDelete: true)
                .Index(t => t.StudentLedgerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentLedgerTransactions", "StudentLedgerId", "dbo.StudentLedgers");
            DropForeignKey("dbo.StudentLedgers", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentLedgerTransactions", new[] { "StudentLedgerId" });
            DropIndex("dbo.StudentLedgers", new[] { "StudentId" });
            DropTable("dbo.StudentLedgerTransactions");
            DropTable("dbo.StudentLedgers");
        }
    }
}
