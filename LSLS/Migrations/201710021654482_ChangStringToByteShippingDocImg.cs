namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangStringToByteShippingDocImg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransportationInfs", "ShippingDocImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransportationInfs", "ShippingDocImage");
        }
    }
}
