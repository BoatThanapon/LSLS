namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInitailModel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentFileDetails", "PaymentDocId", "dbo.PaymentDocuments");
            DropIndex("dbo.PaymentFileDetails", new[] { "PaymentDocId" });
            DropTable("dbo.PaymentDocuments");
            DropTable("dbo.PaymentFileDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PaymentFileDetails",
                c => new
                    {
                        PaymentFileId = c.Guid(nullable: false),
                        PaymentFileName = c.String(),
                        Extension = c.String(),
                        LastModified = c.DateTime(),
                        PaymentDocId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentFileId);
            
            CreateTable(
                "dbo.PaymentDocuments",
                c => new
                    {
                        PaymentDocId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.PaymentDocId);
            
            CreateIndex("dbo.PaymentFileDetails", "PaymentDocId");
            AddForeignKey("dbo.PaymentFileDetails", "PaymentDocId", "dbo.PaymentDocuments", "PaymentDocId", cascadeDelete: true);
        }
    }
}
