namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentDocuments", "PaymentDocument_PaymentDocId", c => c.Guid());
            CreateIndex("dbo.PaymentDocuments", "PaymentDocument_PaymentDocId");
            AddForeignKey("dbo.PaymentDocuments", "PaymentDocument_PaymentDocId", "dbo.PaymentDocuments", "PaymentDocId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentDocuments", "PaymentDocument_PaymentDocId", "dbo.PaymentDocuments");
            DropIndex("dbo.PaymentDocuments", new[] { "PaymentDocument_PaymentDocId" });
            DropColumn("dbo.PaymentDocuments", "PaymentDocument_PaymentDocId");
        }
    }
}
