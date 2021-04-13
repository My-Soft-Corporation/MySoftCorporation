namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_CourseTimingAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseTimings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        StartingTime = c.DateTime(nullable: false),
                        EndingTime = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Latitude = c.String(nullable: false, maxLength: 150),
                        Longitude = c.String(nullable: false, maxLength: 150),
                        ModifiedOn = c.DateTime(nullable: false),
                        AddedById = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AddedById, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => new { t.CourseId, t.StartingTime }, unique: true, name: "UniqueCourseAndStartTime")
                .Index(t => t.AddedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseTimings", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseTimings", "AddedById", "dbo.AspNetUsers");
            DropIndex("dbo.CourseTimings", new[] { "AddedById" });
            DropIndex("dbo.CourseTimings", "UniqueCourseAndStartTime");
            DropTable("dbo.CourseTimings");
        }
    }
}
