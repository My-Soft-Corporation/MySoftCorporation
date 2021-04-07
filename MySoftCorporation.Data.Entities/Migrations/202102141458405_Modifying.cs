namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifying : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeePictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .Index(t => t.PictureID);
            
            CreateTable(
                "dbo.UserPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .Index(t => t.PictureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPictures", "PictureID", "dbo.Pictures");
            DropForeignKey("dbo.EmployeePictures", "PictureID", "dbo.Pictures");
            DropIndex("dbo.UserPictures", new[] { "PictureID" });
            DropIndex("dbo.EmployeePictures", new[] { "PictureID" });
            DropTable("dbo.UserPictures");
            DropTable("dbo.EmployeePictures");
        }
    }
}
