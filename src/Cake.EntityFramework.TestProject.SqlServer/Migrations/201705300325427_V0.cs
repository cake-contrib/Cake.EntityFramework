namespace Cake.EntityFramework.TestProject.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "Student_Id", "dbo.Students");
            DropIndex("dbo.Classes", new[] { "Student_Id" });
            DropTable("dbo.Students");
            DropTable("dbo.Classes");
        }
    }
}
