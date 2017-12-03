namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentDocumentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentDocuments",
                c => new
                    {
                        PaymentDocId = c.Int(nullable: false, identity: true),
                        DescriptionPayment = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.PaymentDocId);
            
            AddColumn("dbo.FileDetails", "PaymentDocument_PaymentDocId", c => c.Int());
            CreateIndex("dbo.FileDetails", "PaymentDocument_PaymentDocId");
            AddForeignKey("dbo.FileDetails", "PaymentDocument_PaymentDocId", "dbo.PaymentDocuments", "PaymentDocId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileDetails", "PaymentDocument_PaymentDocId", "dbo.PaymentDocuments");
            DropIndex("dbo.FileDetails", new[] { "PaymentDocument_PaymentDocId" });
            DropColumn("dbo.FileDetails", "PaymentDocument_PaymentDocId");
            DropTable("dbo.PaymentDocuments");
        }
    }
}
