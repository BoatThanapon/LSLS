namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobAssingments",
                c => new
                    {
                        JobAssignmentId = c.Int(nullable: false, identity: true),
                        ShippingId = c.Int(nullable: false),
                        TruckDriverId = c.Int(nullable: false),
                        JobAssignmentDateTime = c.DateTime(nullable: false),
                        StartingPointJob = c.String(),
                        LatitudeStartJob = c.Single(nullable: false),
                        LongitudeStartJob = c.Single(nullable: false),
                        DestinationJob = c.String(),
                        LatitudeDesJob = c.Single(nullable: false),
                        LongitudeDesJob = c.Single(nullable: false),
                        JobAssignmentStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.JobAssignmentId)
                .ForeignKey("dbo.TransportationInfs", t => t.ShippingId, cascadeDelete: true)
                .ForeignKey("dbo.TruckDrivers", t => t.TruckDriverId, cascadeDelete: true)
                .Index(t => t.ShippingId)
                .Index(t => t.TruckDriverId);
            
            CreateTable(
                "dbo.TransportationInfs",
                c => new
                    {
                        ShippingId = c.Int(nullable: false, identity: true),
                        Employer = c.String(nullable: false, maxLength: 30),
                        OrderDate = c.DateTime(nullable: false),
                        DateOfTransportation = c.DateTime(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 30),
                        StartingPoint = c.String(nullable: false),
                        Destination = c.String(nullable: false),
                        RecieverName = c.String(nullable: false),
                        StatusOfTransportation = c.Boolean(nullable: false),
                        ShipingDocImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.ShippingId);
            
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
                "dbo.TruckLocations",
                c => new
                    {
                        TruckLocationId = c.Int(nullable: false, identity: true),
                        Latitude = c.Single(nullable: false),
                        Longitude = c.Single(nullable: false),
                        TruckCurrentTime = c.DateTime(),
                        TruckCurrentAddress = c.String(),
                        TruckDriverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TruckLocationId)
                .ForeignKey("dbo.TruckDrivers", t => t.TruckDriverId, cascadeDelete: true)
                .Index(t => t.TruckDriverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TruckLocations", "TruckDriverId", "dbo.TruckDrivers");
            DropForeignKey("dbo.JobAssingments", "TruckDriverId", "dbo.TruckDrivers");
            DropForeignKey("dbo.JobAssingments", "ShippingId", "dbo.TransportationInfs");
            DropIndex("dbo.TruckLocations", new[] { "TruckDriverId" });
            DropIndex("dbo.JobAssingments", new[] { "TruckDriverId" });
            DropIndex("dbo.JobAssingments", new[] { "ShippingId" });
            DropTable("dbo.TruckLocations");
            DropTable("dbo.Staffs");
            DropTable("dbo.TruckDrivers");
            DropTable("dbo.TransportationInfs");
            DropTable("dbo.JobAssingments");
        }
    }
}
