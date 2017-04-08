using System.Data.Entity.Migrations;

namespace Cake.EntityFramework.TestProject.Postgres.Migrations
{
    public partial class V0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "public.Classes",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Students", t => t.Student_Id)
                .Index(t => t.Student_Id);

            CreateTable(
                    "public.Students",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("public.Classes", "Student_Id", "public.Students");
            DropIndex("public.Classes", new[] {"Student_Id"});
            DropTable("public.Students");
            DropTable("public.Classes");
        }
    }
}