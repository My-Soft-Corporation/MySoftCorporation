namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingDepartmentForeignKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Terms", "DepartmentID");
            AddForeignKey("dbo.Terms", "DepartmentID", "dbo.Departments", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Terms", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Terms", new[] { "DepartmentID" });
        }
    }
}
