namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Course_RemoveRestriction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "CategoryID", "dbo.CourseCategories");
            DropIndex("dbo.Courses", new[] { "CategoryID" });
            AlterColumn("dbo.Courses", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "CategoryID");
            AddForeignKey("dbo.Courses", "CategoryID", "dbo.CourseCategories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CategoryID", "dbo.CourseCategories");
            DropIndex("dbo.Courses", new[] { "CategoryID" });
            AlterColumn("dbo.Courses", "CategoryID", c => c.Int());
            CreateIndex("dbo.Courses", "CategoryID");
            AddForeignKey("dbo.Courses", "CategoryID", "dbo.CourseCategories", "ID");
        }
    }
}
