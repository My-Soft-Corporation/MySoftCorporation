namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPhotoColumnToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Photo");
        }
    }
}
