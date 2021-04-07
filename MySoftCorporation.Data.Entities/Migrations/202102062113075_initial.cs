namespace MySoftCorporation.Data.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StateID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Provinces", t => t.StateID, cascadeDelete: true)
                .Index(t => t.StateID);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CountryID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Duration = c.Int(nullable: false),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmployeeID = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        AddedBy = c.Int(nullable: false),
                        AddedOn = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        CNIC = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false),
                        BloodGroup = c.String(nullable: false, maxLength: 10),
                        Contact = c.String(nullable: false, maxLength: 30),
                        City = c.String(nullable: false, maxLength: 100),
                        PresentAddress = c.String(nullable: false, maxLength: 100),
                        PermenantAddress = c.String(nullable: false, maxLength: 100),
                        Gender = c.String(nullable: false, maxLength: 10),
                        ModifiedOn = c.DateTime(nullable: false),
                        AddedBy = c.Int(nullable: false),
                        AddedOn = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 100),
                        EmployeeID = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        AddedBy = c.Int(nullable: false),
                        AddedOn = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        CNIC = c.String(nullable: false, maxLength: 50),
                        FatherName = c.String(nullable: false, maxLength: 50),
                        FatherCNIC = c.String(nullable: false, maxLength: 50),
                        FatherProfession = c.String(nullable: false, maxLength: 50),
                        StudentContact = c.String(nullable: false, maxLength: 30),
                        FatherContact = c.String(nullable: false, maxLength: 30),
                        EmergencyContact = c.String(nullable: false, maxLength: 30),
                        DateOfBirth = c.DateTime(nullable: false),
                        BloodGroup = c.String(nullable: false, maxLength: 10),
                        Gender = c.String(nullable: false, maxLength: 10),
                        Country = c.String(nullable: false, maxLength: 50),
                        State = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 80),
                        PresentAddress = c.String(nullable: false, maxLength: 100),
                        PermenantAddress = c.String(nullable: false, maxLength: 100),
                        EmployeeID = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        AddedBy = c.Int(nullable: false),
                        AddedOn = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Terms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartingDate = c.DateTime(nullable: false),
                        EndingDate = c.DateTime(nullable: false),
                        Months = c.Int(nullable: false),
                        Basic = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransportAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DinnerAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RentAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RiskAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConveyanceAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DepartmentID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        AddedBy = c.Int(nullable: false),
                        AddedOn = c.DateTime(nullable: false),
                        IP = c.String(nullable: false, maxLength: 50),
                        Agent = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Terms", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Students", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Departments", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Courses", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Cities", "StateID", "dbo.Provinces");
            DropForeignKey("dbo.Provinces", "CountryID", "dbo.Countries");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Terms", new[] { "EmployeeID" });
            DropIndex("dbo.Students", new[] { "EmployeeID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Departments", new[] { "EmployeeID" });
            DropIndex("dbo.Courses", new[] { "EmployeeID" });
            DropIndex("dbo.Provinces", new[] { "CountryID" });
            DropIndex("dbo.Cities", new[] { "StateID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Terms");
            DropTable("dbo.Students");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.Courses");
            DropTable("dbo.Countries");
            DropTable("dbo.Provinces");
            DropTable("dbo.Cities");
        }
    }
}
