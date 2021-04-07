namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingAddedByInEmployeeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "AddedBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "AddedBy", c => c.Int(nullable: false));
        }
    }
}
