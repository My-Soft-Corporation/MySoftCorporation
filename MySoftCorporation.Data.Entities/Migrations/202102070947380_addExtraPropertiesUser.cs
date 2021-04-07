namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addExtraPropertiesUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "CNIC", c => c.String());
            AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "Contact", c => c.String());
            AddColumn("dbo.AspNetUsers", "PermenantAddress", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddColumn("dbo.AspNetUsers", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "ModifiedBy", c => c.String());
            AddColumn("dbo.AspNetUsers", "IP", c => c.String());
            AddColumn("dbo.AspNetUsers", "Agent", c => c.String());
            AddColumn("dbo.AspNetUsers", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Location");
            DropColumn("dbo.AspNetUsers", "Agent");
            DropColumn("dbo.AspNetUsers", "IP");
            DropColumn("dbo.AspNetUsers", "ModifiedBy");
            DropColumn("dbo.AspNetUsers", "ModifiedOn");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "PermenantAddress");
            DropColumn("dbo.AspNetUsers", "Contact");
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
            DropColumn("dbo.AspNetUsers", "CNIC");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
