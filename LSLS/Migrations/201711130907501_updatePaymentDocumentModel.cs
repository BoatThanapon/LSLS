namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePaymentDocumentModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PaymentDocuments", "PaymentFileCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentDocuments", "PaymentFileCategory", c => c.String());
        }
    }
}
