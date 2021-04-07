namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingColumnName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "Province_ID", "dbo.Provinces");
            DropIndex("dbo.Cities", new[] { "Province_ID" });
            RenameColumn(table: "dbo.Cities", name: "Province_ID", newName: "ProvinceID");
            AlterColumn("dbo.Cities", "ProvinceID", c => c.Int(nullable: false));
            CreateIndex("dbo.Cities", "ProvinceID");
            AddForeignKey("dbo.Cities", "ProvinceID", "dbo.Provinces", "ID", cascadeDelete: true);
            DropColumn("dbo.Cities", "StateID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cities", "StateID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Cities", "ProvinceID", "dbo.Provinces");
            DropIndex("dbo.Cities", new[] { "ProvinceID" });
            AlterColumn("dbo.Cities", "ProvinceID", c => c.Int());
            RenameColumn(table: "dbo.Cities", name: "ProvinceID", newName: "Province_ID");
            CreateIndex("dbo.Cities", "Province_ID");
            AddForeignKey("dbo.Cities", "Province_ID", "dbo.Provinces", "ID");
        }
    }
}
