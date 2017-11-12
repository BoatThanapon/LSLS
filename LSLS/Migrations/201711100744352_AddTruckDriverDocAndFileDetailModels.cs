namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTruckDriverDocAndFileDetailModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileDetails",
                c => new
                    {
                        FileId = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        TruckDriverDocId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.TruckDriverDocuments", t => t.TruckDriverDocId, cascadeDelete: true)
                .Index(t => t.TruckDriverDocId);
            
            CreateTable(
                "dbo.TruckDriverDocuments",
                c => new
                    {
                        TruckDriverDocId = c.Int(nullable: false, identity: true),
                        TruckDriverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TruckDriverDocId)
                .ForeignKey("dbo.TruckDrivers", t => t.TruckDriverId, cascadeDelete: true)
                .Index(t => t.TruckDriverId);
            
            DropTable("dbo.FileOnlines");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FileOnlines",
                c => new
                    {
                        FileId = c.String(nullable: false, maxLength: 128),
                        FileName = c.String(),
                        FileSize = c.Long(),
                        FileVersion = c.Long(),
                        FileCreatedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.FileId);
            
            DropForeignKey("dbo.TruckDriverDocuments", "TruckDriverId", "dbo.TruckDrivers");
            DropForeignKey("dbo.FileDetails", "TruckDriverDocId", "dbo.TruckDriverDocuments");
            DropIndex("dbo.TruckDriverDocuments", new[] { "TruckDriverId" });
            DropIndex("dbo.FileDetails", new[] { "TruckDriverDocId" });
            DropTable("dbo.TruckDriverDocuments");
            DropTable("dbo.FileDetails");
        }
    }
}
