namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTruckDriverDocumentAndFileDetailModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileDetails", "FileCategory", c => c.String());
            AddColumn("dbo.FileDetails", "LastModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileDetails", "LastModified");
            DropColumn("dbo.FileDetails", "FileCategory");
        }
    }
}
