namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingEmployeeIDToUserID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Departments", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Students", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Terms", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Terms", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.Courses", new[] { "EmployeeID" });
            DropIndex("dbo.Employees", new[] { "UserID" });
            DropIndex("dbo.Employees", new[] { "CityID" });
            DropIndex("dbo.Departments", new[] { "EmployeeID" });
            DropIndex("dbo.Students", new[] { "EmployeeID" });
            DropIndex("dbo.Terms", new[] { "DepartmentID" });
            DropIndex("dbo.Terms", new[] { "EmployeeID" });
            AddColumn("dbo.Courses", "UserID", c => c.String(maxLength: 128));
            AddColumn("dbo.Departments", "UserID", c => c.String(maxLength: 128));
            AddColumn("dbo.Students", "UserID", c => c.String(maxLength: 128));
            AddColumn("dbo.Terms", "RoleID", c => c.Int(nullable: false));
            AddColumn("dbo.Terms", "UserID", c => c.String(maxLength: 128));
            AddColumn("dbo.Terms", "Role_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courses", "UserID");
            CreateIndex("dbo.Employees", "CityID", unique: true);
            CreateIndex("dbo.Employees", "UserID");
            CreateIndex("dbo.Departments", "UserID");
            CreateIndex("dbo.Students", "UserID");
            CreateIndex("dbo.Terms", "UserID");
            CreateIndex("dbo.Terms", "Role_Id");
            AddForeignKey("dbo.Courses", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Departments", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Students", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Terms", "Role_Id", "dbo.AspNetRoles", "Id");
            AddForeignKey("dbo.Terms", "UserID", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Courses", "EmployeeID");
            DropColumn("dbo.Courses", "AddedBy");
            DropColumn("dbo.Courses", "AddedOn");
            DropColumn("dbo.Employees", "AddedBy");
            DropColumn("dbo.Employees", "AddedOn");
            DropColumn("dbo.Departments", "EmployeeID");
            DropColumn("dbo.Departments", "AddedBy");
            DropColumn("dbo.Departments", "AddedOn");
            DropColumn("dbo.Students", "EmployeeID");
            DropColumn("dbo.Students", "AddedBy");
            DropColumn("dbo.Students", "AddedOn");
            DropColumn("dbo.Terms", "DepartmentID");
            DropColumn("dbo.Terms", "EmployeeID");
            DropColumn("dbo.Terms", "AddedBy");
            DropColumn("dbo.Terms", "AddedOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Terms", "AddedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Terms", "AddedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Terms", "EmployeeID", c => c.Int(nullable: false));
            AddColumn("dbo.Terms", "DepartmentID", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "AddedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Students", "AddedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "EmployeeID", c => c.Int(nullable: false));
            AddColumn("dbo.Departments", "AddedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Departments", "AddedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Departments", "EmployeeID", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "AddedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employees", "AddedBy", c => c.String());
            AddColumn("dbo.Courses", "AddedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Courses", "AddedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "EmployeeID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Terms", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Terms", "Role_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.Students", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Departments", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Terms", new[] { "Role_Id" });
            DropIndex("dbo.Terms", new[] { "UserID" });
            DropIndex("dbo.Students", new[] { "UserID" });
            DropIndex("dbo.Departments", new[] { "UserID" });
            DropIndex("dbo.Employees", new[] { "UserID" });
            DropIndex("dbo.Employees", new[] { "CityID" });
            DropIndex("dbo.Courses", new[] { "UserID" });
            DropColumn("dbo.Terms", "Role_Id");
            DropColumn("dbo.Terms", "UserID");
            DropColumn("dbo.Terms", "RoleID");
            DropColumn("dbo.Students", "UserID");
            DropColumn("dbo.Departments", "UserID");
            DropColumn("dbo.Courses", "UserID");
            CreateIndex("dbo.Terms", "EmployeeID");
            CreateIndex("dbo.Terms", "DepartmentID");
            CreateIndex("dbo.Students", "EmployeeID");
            CreateIndex("dbo.Departments", "EmployeeID");
            CreateIndex("dbo.Employees", "CityID");
            CreateIndex("dbo.Employees", "UserID", unique: true);
            CreateIndex("dbo.Courses", "EmployeeID");
            AddForeignKey("dbo.Terms", "EmployeeID", "dbo.Employees", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Terms", "DepartmentID", "dbo.Departments", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Students", "EmployeeID", "dbo.Employees", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Departments", "EmployeeID", "dbo.Employees", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Courses", "EmployeeID", "dbo.Employees", "ID", cascadeDelete: true);
        }
    }
}
