namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateShippingModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransportationInfs", "ShippingNote", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransportationInfs", "ShippingNote");
        }
    }
}
