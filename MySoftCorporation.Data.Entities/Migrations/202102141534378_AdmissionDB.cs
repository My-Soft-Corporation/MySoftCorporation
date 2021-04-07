namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdmissionDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admissions",
                c => new
                    {
                        AdmissionID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.AdmissionID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.StudentID)
                .Index(t => t.CourseID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admissions", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Admissions", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Admissions", "CourseID", "dbo.Courses");
            DropIndex("dbo.Admissions", new[] { "UserID" });
            DropIndex("dbo.Admissions", new[] { "CourseID" });
            DropIndex("dbo.Admissions", new[] { "StudentID" });
            DropTable("dbo.Admissions");
        }
    }
}
