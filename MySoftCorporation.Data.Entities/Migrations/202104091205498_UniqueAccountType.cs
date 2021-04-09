namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueAccountType : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AccountTypes", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.AccountTypes", new[] { "Name" });
        }
    }
}
