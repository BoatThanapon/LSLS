namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertyShippingDocImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransportationInfs", "ShippingDocImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransportationInfs", "ShippingDocImage");
        }
    }
}
