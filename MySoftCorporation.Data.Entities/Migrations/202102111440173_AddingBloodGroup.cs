namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBloodGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BloodGroups",
                c => new
                    {
                        BloodGroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BloodGroupID);
            
            AddColumn("dbo.Students", "BloodGroupID", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "BloodGroupID");
            AddForeignKey("dbo.Students", "BloodGroupID", "dbo.BloodGroups", "BloodGroupID", cascadeDelete: false);
            DropColumn("dbo.Students", "BloodGroup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "BloodGroup", c => c.String(nullable: false, maxLength: 10));
            DropForeignKey("dbo.Students", "BloodGroupID", "dbo.BloodGroups");
            DropIndex("dbo.Students", new[] { "BloodGroupID" });
            DropColumn("dbo.Students", "BloodGroupID");
            DropTable("dbo.BloodGroups");
        }
    }
}
