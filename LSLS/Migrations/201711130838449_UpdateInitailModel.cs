namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInitailModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileDetails", "PaymentDocument_PaymentDocId", "dbo.PaymentDocuments");
            DropIndex("dbo.FileDetails", new[] { "PaymentDocument_PaymentDocId" });
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
                .PrimaryKey(t => t.PaymentFileId)
                .ForeignKey("dbo.PaymentDocuments", t => t.PaymentDocId, cascadeDelete: true)
                .Index(t => t.PaymentDocId);
            
            DropColumn("dbo.FileDetails", "PaymentDocument_PaymentDocId");
            DropColumn("dbo.PaymentDocuments", "DescriptionPayment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentDocuments", "DescriptionPayment", c => c.String(maxLength: 200));
            AddColumn("dbo.FileDetails", "PaymentDocument_PaymentDocId", c => c.Int());
            DropForeignKey("dbo.PaymentFileDetails", "PaymentDocId", "dbo.PaymentDocuments");
            DropIndex("dbo.PaymentFileDetails", new[] { "PaymentDocId" });
            DropTable("dbo.PaymentFileDetails");
            CreateIndex("dbo.FileDetails", "PaymentDocument_PaymentDocId");
            AddForeignKey("dbo.FileDetails", "PaymentDocument_PaymentDocId", "dbo.PaymentDocuments", "PaymentDocId");
        }
    }
}
