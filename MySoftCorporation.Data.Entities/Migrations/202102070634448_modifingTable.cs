namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifingTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "StateID", "dbo.Provinces");
            DropIndex("dbo.Cities", new[] { "StateID" });
            AddColumn("dbo.Cities", "Province_ID", c => c.Int());
            CreateIndex("dbo.Cities", "Province_ID");
            AddForeignKey("dbo.Cities", "Province_ID", "dbo.Provinces", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "Province_ID", "dbo.Provinces");
            DropIndex("dbo.Cities", new[] { "Province_ID" });
            DropColumn("dbo.Cities", "Province_ID");
            CreateIndex("dbo.Cities", "StateID");
            AddForeignKey("dbo.Cities", "StateID", "dbo.Provinces", "ID", cascadeDelete: true);
        }
    }
}
