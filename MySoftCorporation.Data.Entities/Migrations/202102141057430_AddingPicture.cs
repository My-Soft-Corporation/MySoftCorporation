namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPicture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CoursePictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PictureID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.PictureID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                        UserID = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CoursePictures", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.CoursePictures", "PictureID", "dbo.Pictures");
            DropForeignKey("dbo.Pictures", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Pictures", new[] { "UserID" });
            DropIndex("dbo.CoursePictures", new[] { "CourseID" });
            DropIndex("dbo.CoursePictures", new[] { "PictureID" });
            DropTable("dbo.Pictures");
            DropTable("dbo.CoursePictures");
        }
    }
}
