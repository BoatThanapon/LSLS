namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitailModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        StaffEmployeeId = c.String(nullable: false),
                        StaffUsername = c.String(nullable: false, maxLength: 15),
                        StaffPassword = c.String(nullable: false, maxLength: 15),
                        StaffConfirmPassword = c.String(nullable: false, maxLength: 15),
                        StaffFullname = c.String(nullable: false, maxLength: 30),
                        StaffGender = c.String(nullable: false),
                        StaffCitizenId = c.String(nullable: false),
                        StaffBirthdate = c.String(nullable: false),
                        StaffAddress = c.String(nullable: false, maxLength: 200),
                        StaffEmail = c.String(nullable: false),
                        StaffTelephoneNo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StaffId);
            
            CreateTable(
                "dbo.TruckDrivers",
                c => new
                    {
                        TruckDriverId = c.Int(nullable: false, identity: true),
                        TruckDriverUsername = c.String(nullable: false, maxLength: 15),
                        TruckDriverPassword = c.String(nullable: false, maxLength: 15),
                        TruckDriverConfirmPassword = c.String(nullable: false, maxLength: 15),
                        TruckDriverFullname = c.String(nullable: false, maxLength: 50),
                        TruckDriverGender = c.String(nullable: false),
                        TruckDriverCitizenId = c.String(nullable: false),
                        TruckDriverDriverLicenseId = c.String(nullable: false),
                        TruckDriverAddress = c.String(nullable: false, maxLength: 200),
                        TruckDriverBirthdate = c.String(nullable: false),
                        TruckDriverEmail = c.String(nullable: false),
                        TruckDriverTelephoneNo = c.String(nullable: false),
                        TruckId = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.TruckDriverId);
            
            CreateTable(
                "dbo.TruckLocations",
                c => new
                    {
                        TruckLocationId = c.Int(nullable: false, identity: true),
                        TruckDriverId = c.Int(nullable: false),
                        Latitude = c.Single(nullable: false),
                        Longitude = c.Single(nullable: false),
                        TruckCurrentTime = c.DateTime(),
                        TruckCurrentAddress = c.String(),
                    })
                .PrimaryKey(t => t.TruckLocationId)
                .ForeignKey("dbo.TruckDrivers", t => t.TruckDriverId, cascadeDelete: true)
                .Index(t => t.TruckDriverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TruckLocations", "TruckDriverId", "dbo.TruckDrivers");
            DropIndex("dbo.TruckLocations", new[] { "TruckDriverId" });
            DropTable("dbo.TruckLocations");
            DropTable("dbo.TruckDrivers");
            DropTable("dbo.Staffs");
        }
    }
}
