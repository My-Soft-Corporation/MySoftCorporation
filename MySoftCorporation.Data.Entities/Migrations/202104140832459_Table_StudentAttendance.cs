namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_StudentAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentAttendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ClockIn = c.DateTime(),
                        CheckOut = c.DateTime(),
                        CheckIn = c.DateTime(),
                        ClockOut = c.DateTime(),
                        StudentId = c.Int(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 4),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 80),
                        Latitude = c.String(nullable: false, maxLength: 50),
                        Longitude = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentAttendances", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentAttendances", new[] { "StudentId" });
            DropTable("dbo.StudentAttendances");
        }
    }
}
