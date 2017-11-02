namespace LSLS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileOnlineModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileOnlines",
                c => new
                    {
                        FileId = c.String(nullable: false, maxLength: 128),
                        FileName = c.String(),
                        FileSize = c.Long(),
                        FileVersion = c.Long(),
                        FileCreatedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FileOnlines");
        }
    }
}
