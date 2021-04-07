namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAuxToCourseCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseCategories", "UserID", c => c.String(maxLength: 128));
            AddColumn("dbo.CourseCategories", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.CourseCategories", "IP", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.CourseCategories", "Agent", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.CourseCategories", "Location", c => c.String(nullable: false, maxLength: 150));
            CreateIndex("dbo.CourseCategories", "UserID");
            AddForeignKey("dbo.CourseCategories", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseCategories", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.CourseCategories", new[] { "UserID" });
            DropColumn("dbo.CourseCategories", "Location");
            DropColumn("dbo.CourseCategories", "Agent");
            DropColumn("dbo.CourseCategories", "IP");
            DropColumn("dbo.CourseCategories", "ModifiedOn");
            DropColumn("dbo.CourseCategories", "UserID");
        }
    }
}
