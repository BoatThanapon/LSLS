namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteShippingDocIng : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TransportationInfs", "ShippingDocImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransportationInfs", "ShippingDocImage", c => c.String());
        }
    }
}
