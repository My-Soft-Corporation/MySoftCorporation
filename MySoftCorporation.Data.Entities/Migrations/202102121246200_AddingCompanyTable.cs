namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCompanyTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Cell = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 400),
                        Email = c.String(nullable: false, maxLength: 200),
                        UserID = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            AlterColumn("dbo.EAttendances", "ClockOut", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Companies", new[] { "UserID" });
            AlterColumn("dbo.EAttendances", "ClockOut", c => c.DateTime(nullable: false));
            DropTable("dbo.Companies");
        }
    }
}
