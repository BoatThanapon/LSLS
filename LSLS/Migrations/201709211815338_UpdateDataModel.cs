namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.JobAssingments", newName: "JobAssignments");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.JobAssignments", newName: "JobAssingments");
        }
    }
}
