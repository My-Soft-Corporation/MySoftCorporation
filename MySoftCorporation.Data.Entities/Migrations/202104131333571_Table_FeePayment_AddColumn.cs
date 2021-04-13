namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_FeePayment_AddColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeePayments", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.FeePayments", "TransactionID", c => c.String(nullable: false));
            AddColumn("dbo.FeePayments", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FeePayments", "DateTime");
            DropColumn("dbo.FeePayments", "TransactionID");
            DropColumn("dbo.FeePayments", "Amount");
        }
    }
}
