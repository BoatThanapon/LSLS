namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInitialModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobAssignments", "StartingPointJob", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.JobAssignments", "DestinationJob", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobAssignments", "DestinationJob", c => c.String(nullable: false));
            AlterColumn("dbo.JobAssignments", "StartingPointJob", c => c.String(nullable: false));
        }
    }
}
