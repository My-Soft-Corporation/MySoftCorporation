namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseTableModification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CoursePictures", "PictureID", "dbo.Pictures");
            DropForeignKey("dbo.CoursePictures", "CourseID", "dbo.Courses");
            DropIndex("dbo.CoursePictures", new[] { "PictureID" });
            DropIndex("dbo.CoursePictures", new[] { "CourseID" });
            AddColumn("dbo.Courses", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Courses", "EndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Courses", "CategoryID", c => c.Int());
            AlterColumn("dbo.CourseCategories", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Courses", "CategoryID");
            AddForeignKey("dbo.Courses", "CategoryID", "dbo.CourseCategories", "ID");
            DropColumn("dbo.Courses", "CourseCategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "CourseCategoryID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Courses", "CategoryID", "dbo.CourseCategories");
            DropIndex("dbo.Courses", new[] { "CategoryID" });
            AlterColumn("dbo.CourseCategories", "Name", c => c.String());
            DropColumn("dbo.Courses", "CategoryID");
            DropColumn("dbo.Courses", "EndTime");
            DropColumn("dbo.Courses", "StartTime");
            CreateIndex("dbo.CoursePictures", "CourseID");
            CreateIndex("dbo.CoursePictures", "PictureID");
            AddForeignKey("dbo.CoursePictures", "CourseID", "dbo.Courses", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CoursePictures", "PictureID", "dbo.Pictures", "ID", cascadeDelete: true);
        }
    }
}
