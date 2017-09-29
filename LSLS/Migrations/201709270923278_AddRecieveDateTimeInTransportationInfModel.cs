namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecieveDateTimeInTransportationInfModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransportationInfs", "RecieveDateTime", c => c.DateTime());
            DropColumn("dbo.TransportationInfs", "ShipingDocImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransportationInfs", "ShipingDocImageUrl", c => c.String());
            DropColumn("dbo.TransportationInfs", "RecieveDateTime");
        }
    }
}
