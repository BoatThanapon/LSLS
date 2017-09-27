namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAnnotationJobAssignmentModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobAssignments", "StartingPointJob", c => c.String(nullable: false));
            AlterColumn("dbo.JobAssignments", "DestinationJob", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobAssignments", "DestinationJob", c => c.String());
            AlterColumn("dbo.JobAssignments", "StartingPointJob", c => c.String());
        }
    }
}
