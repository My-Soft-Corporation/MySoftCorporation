namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingConstraintsOnAdmission : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Admissions", new[] { "StudentID" });
            DropIndex("dbo.Admissions", new[] { "CourseID" });
            AddColumn("dbo.Admissions", "IsConfirmed", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Admissions", new[] { "StudentID", "CourseID" }, unique: true, name: "StudentAndCourseUnique");
            CreateIndex("dbo.Students", "CNIC", unique: true);
            CreateIndex("dbo.Students", "StudentContact", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Students", new[] { "StudentContact" });
            DropIndex("dbo.Students", new[] { "CNIC" });
            DropIndex("dbo.Admissions", "StudentAndCourseUnique");
            DropColumn("dbo.Admissions", "IsConfirmed");
            CreateIndex("dbo.Admissions", "CourseID");
            CreateIndex("dbo.Admissions", "StudentID");
        }
    }
}
