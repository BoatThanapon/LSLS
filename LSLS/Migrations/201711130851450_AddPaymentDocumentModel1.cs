namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentDocumentModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentDocuments",
                c => new
                    {
                        PaymentDocId = c.Guid(nullable: false),
                        PaymentFileName = c.String(),
                        Extension = c.String(),
                        PaymentFileCategory = c.String(),
                        PaymentLastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.PaymentDocId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentDocuments");
        }
    }
}
