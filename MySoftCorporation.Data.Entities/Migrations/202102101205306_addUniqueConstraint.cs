namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUniqueConstraint : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Employees", new[] { "UserID" });
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Courses", "Duration", c => c.Byte(nullable: false));
            CreateIndex("dbo.Courses", "Title", unique: true);
            CreateIndex("dbo.Employees", "CNIC", unique: true);
            CreateIndex("dbo.Employees", "Contact", unique: true);
            CreateIndex("dbo.Employees", "Email", unique: true);
            CreateIndex("dbo.Employees", "UserID", unique: true);
            CreateIndex("dbo.Departments", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Departments", new[] { "Name" });
            DropIndex("dbo.Employees", new[] { "UserID" });
            DropIndex("dbo.Employees", new[] { "Email" });
            DropIndex("dbo.Employees", new[] { "Contact" });
            DropIndex("dbo.Employees", new[] { "CNIC" });
            DropIndex("dbo.Courses", new[] { "Title" });
            AlterColumn("dbo.Courses", "Duration", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "Title", c => c.String());
            CreateIndex("dbo.Employees", "UserID");
        }
    }
}
