namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingEmployeeAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EAttendances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClockIn = c.DateTime(nullable: false),
                        CheckOut = c.DateTime(),
                        CheckIn = c.DateTime(),
                        ClockOut = c.DateTime(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 4),
                        EmployeeUserID = c.String(),
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
            DropForeignKey("dbo.EAttendances", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.EAttendances", new[] { "UserID" });
            DropTable("dbo.EAttendances");
        }
    }
}
