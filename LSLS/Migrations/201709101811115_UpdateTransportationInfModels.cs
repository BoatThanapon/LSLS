namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTransportationInfModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransportationInfs", "JobIsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransportationInfs", "JobIsActive");
        }
    }
}
