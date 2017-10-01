namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTransportationInfClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransportationInfs", "ReceiverName", c => c.String(nullable: false));
            AddColumn("dbo.TransportationInfs", "ReceiveDateTime", c => c.DateTime());
            DropColumn("dbo.TransportationInfs", "RecieverName");
            DropColumn("dbo.TransportationInfs", "RecieveDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransportationInfs", "RecieveDateTime", c => c.DateTime());
            AddColumn("dbo.TransportationInfs", "RecieverName", c => c.String(nullable: false));
            DropColumn("dbo.TransportationInfs", "ReceiveDateTime");
            DropColumn("dbo.TransportationInfs", "ReceiverName");
        }
    }
}
